//-----------------------------------------------------------------------
// <copyright file="RoomConfig.cs" company="Google">
//
// Copyright 2016 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase.Database;


public class RoomConfig : MonoBehaviour {

    MultiplayerContol multiplayer;
    UILobby lobbyUI;
    CanvasGroup buttonGroup;
    Button button;
    Text label;
    ARCoreController arCoreController;
    ARKitController arKitController;
    UIControl uiControl;

    enum RoomStatus { Empty, Full, Locked, Waiting, Done }
    RoomStatus roomStatus = RoomStatus.Empty;

    Image lobbyIcon;
    Image player2Icon;

    bool waitingToJoin = false;
    bool canReJoin = false;

    public int roomNumber = -1;

    bool isActive = false;
    float activeTimer = 0;
    float timerDuration = 0.25f;
    public bool IsActive {
        get { return isActive; }
        set {
            isActive = value;
            activeTimer = Mathf.Clamp01(activeTimer);
            if (isActive)
                StartCoroutine(PingRoom());
        }
    }

    bool listenerAdded = false;

    float loadingTimer = 0.0f;
    int loadingIndex = 0;

    private void Start() {
        multiplayer = MultiplayerContol.Instance;
        label = transform.Find("Label").GetComponent<Text>();
        buttonGroup = GetComponent<CanvasGroup>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        lobbyIcon = transform.Find("Icon").GetComponent<Image>();

        uiControl = GameObject.Find("GameUI").GetComponent<UIControl>();
    }

    public void Init(UILobby lobby) {
        if (listenerAdded) return;

        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            arKitController = GameManager.Instance.ARKitController;
        } else {
            arCoreController = GameManager.Instance.ARCoreController;
        }

        multiplayer.AddRoomStatusListener(roomNumber, NetworkStatusUpdate);
        listenerAdded = true;
        lobbyUI = lobby;
    }

    public void Reset() {
        gameObject.SetActive(true);
    }

    IEnumerator PingRoom() {
        while (isActive) {
            if (roomStatus == RoomStatus.Done)
                multiplayer.UpdateRoomStatus(roomNumber);
            
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void Update() {
        if (activeTimer >= 0.0f && activeTimer <= 1.0f) {
            activeTimer += Time.deltaTime / (isActive ? timerDuration : -timerDuration);
            float amount = Mathf.SmoothStep(0, 1, activeTimer);
            buttonGroup.alpha = amount;
        }

        if (roomStatus == RoomStatus.Locked || roomStatus == RoomStatus.Done) {
            loadingTimer -= Time.deltaTime;
            if (loadingTimer < 0) {
                loadingIndex = (loadingIndex + 1) % lobbyUI.iconLoading.Length;
                lobbyIcon.sprite = lobbyUI.iconLoading[loadingIndex];
                loadingTimer = 0.15f;
            }
        }
    }

    void OnClick() {
        if (!isActive)
            return;
        multiplayer.RoomNumber = roomNumber;

        button.interactable = false;
        label.text = "Checking Lobby...";
        multiplayer.GetRoomStatus(roomNumber, UpdateStatusBeforeJoining);

        GetComponentInParent<UIControl>().PlayButtonSound();

    }

    void UpdateStatusBeforeJoining(string status) {
        Debug.Log("Lobby Rooms Status is " + status);
        if (status == "empty") {
            Debug.Log("Hosting Game");
            GameManager.Instance.HostGame(roomNumber);

        } else if (status == "waiting") {
            multiplayer.SetRoomStatus(roomNumber, "locked");
            waitingToJoin = true;

            if (Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.WindowsEditor) {
                lobbyUI.IsVisible = false;
                GameManager.Instance.JoinEditorGame(roomNumber);

            } else {
                multiplayer.GetRoomCloudId(roomNumber, GotCloudID);
            }

        } else if (status == "full" && canReJoin) {
            lobbyUI.IsVisible = false;
        } else {
            GameManager.Instance.ShowMessage("Lobby is not available! Try another.", 5);
        }
    }

    public void GotCloudID(string cloudID) {
        uiControl.ShowTips(true);
        uiControl.ShowProgress(true);

        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            arKitController.ResolveAnchor(cloudID, GotAnchor);
        } else {
            arCoreController.ResolveAnchor(cloudID, GotAnchor);
        }
    }

    public void GotAnchor(Transform newWorldAnchor) {
        multiplayer.SetRoomStatus(roomNumber, "full");
        GameManager.Instance.JoinGame(roomNumber, newWorldAnchor);
        IsActive = false;
    }

    public void NetworkStatusUpdate(object sender2, ValueChangedEventArgs data) {
        if (data.Snapshot.Exists) {

            string status = "empty";

            if (data.Snapshot.Value.Equals(null)) {
                Debug.Log("Data is null");
                return;
            }
            status = data.Snapshot.Value.ToString();
            UpdateStatus(status);
        }
    }

    void UpdateStatus(string status) {
        Debug.Log("Room Status is changed to " + status);
        switch (status) {
            case "blank":
                multiplayer.SetRoomStatus(roomNumber, "empty");
                break;
            case "empty":
                roomStatus = RoomStatus.Empty;
                button.interactable = true;

                label.text = "New Game";
                lobbyIcon.sprite = lobbyUI.iconEmpty;

                break;
            case "full":
                roomStatus = RoomStatus.Full;
                button.interactable = false;

                label.text = "In Use\nSelect another lobby";

                label.text = "Full";
                lobbyIcon.sprite = lobbyUI.iconFull;

                if (waitingToJoin) {
                    lobbyUI.IsVisible = false;
                    waitingToJoin = false;
                }
                multiplayer.GetPlayerNames(roomNumber, CheckPlayers);
                break;
            case "locked":
                roomStatus = RoomStatus.Locked;
                button.interactable = false;

                label.text = "Creating game";
                lobbyIcon.sprite = lobbyUI.iconLoading[0];

                break;
            case "waiting":
                roomStatus = RoomStatus.Waiting;
                button.interactable = true;

                label.text = "Join";
                lobbyIcon.sprite = lobbyUI.iconJoin;

                break;
            case "done":
                roomStatus = RoomStatus.Done;

                label.text = "Resetting";
                lobbyIcon.sprite = lobbyUI.iconLoading[0];

                break;
            default:
                break;
        }
    }

    void CheckPlayers(List<string> playersInRoom) {
        if (playersInRoom.Contains(GameManager.Instance.LocalID)) {
            button.interactable = true;

            label.text = "RE-JOIN";
            lobbyIcon.sprite = lobbyUI.iconFull;

            canReJoin = true;
        }
    }

    private void OnDestroy() {
        multiplayer.RemoveRoomStatusListener(roomNumber, NetworkStatusUpdate);
    }
}
