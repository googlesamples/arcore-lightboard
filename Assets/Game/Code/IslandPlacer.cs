//-----------------------------------------------------------------------
// <copyright file="IslandFootprint.cs" company="Google">
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
using Firebase.Database;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IslandPlacer : MonoBehaviour  {

    GameManager gameManager;
    Transform markerBase;
    Transform markerShadow;
    Transform markerButton;
    IslandFootprint footprint;
    InputRelay markerInput;
    InputRelay footprintInput;
    IslandPlacer opponentPlacer;

    public GameObject messageBox;
    public Text messageText;
    float messageTimer = 0.0f;

    public Texture playerTexture;
    public Texture opponentTexture;

    AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip placeSound;
	public AudioClip islandPlacerUpSound;
	public AudioClip islandPlacerDownSound;


    bool isDragging = false;
    public bool IsDragging {
        get { return isDragging; }

        set {
            isDragging = value;
        }
    }

    Renderer footprintRenderer;
    bool tooClose = false;
    public bool TooClose {
        set {
            tooClose = value;
            footprintRenderer.material.color = tooClose ? Color.red : Color.white;
            if (tooClose) IsReady = false;
        }
    }

    Renderer buttonRenderer;
    bool isReady = false;

    public bool IsReady {
        get { return isReady; }
        set {
            isReady = value;
			Color buttonColorReady = new Color (72 / 255.0f, 213 / 255.0f, 151 / 255.0f);
			Color buttonColorNotReady = new Color (250 / 255.0f, 70 / 255.0f, 22 / 255.0f);
            buttonRenderer.material.color = isReady ? buttonColorReady : buttonColorNotReady;
        }
    }
    
    Vector3 dragOffset = Vector3.zero;

    bool isVisible = false;
    float visibleAmount = 0.0f;
    bool isComplete = false;

    bool isPlayer;
    string playerName;

    float minDistance = 0.8f;

    float updateTimer = 1.0f;

	public void Init (string newName, bool player) {
        gameManager = GameManager.Instance;
        isPlayer = player;
        playerName = newName;
        gameObject.name = playerName + "'s Placer";
        markerBase = transform.Find("FootprintMarker/ButtonBase");
        markerShadow = transform.Find("FootprintMarker/ButtonShadow");
        markerButton = transform.Find("FootprintMarker/Button");
        buttonRenderer = markerButton.GetComponent<Renderer>();
        audioSource = transform.Find("Audio").GetComponent<AudioSource>();

        GameObject footprintObj = transform.Find("FootprintMarker/Footprint").gameObject;
        footprintRenderer = footprintObj.GetComponent<Renderer>();

        footprintObj.GetComponent<Collider>().enabled = player;
        footprint = footprintObj.gameObject.AddComponent<IslandFootprint>();
        footprint.IsReady = true;
        transform.localScale = Vector3.one * 0.1f;

        messageBox.SetActive(false);

        if (isPlayer) {
            markerButton.gameObject.AddComponent<SphereCollider>();
            markerInput = markerButton.gameObject.AddComponent<InputRelay>();
            markerInput.onPointerClick.AddListener(ButtonClicked);

            footprintInput = footprintObj.gameObject.AddComponent<InputRelay>();
            footprintInput.onPointerDown.AddListener(OnPointerDown);
            footprintInput.onPointerDrag.AddListener(OnPointerDrag);
            footprintInput.onPointerUp.AddListener(OnPointerUp);

            footprint.gameObject.GetComponent<Renderer>().material.mainTexture =
                playerTexture;

        } else {
            markerBase.GetComponent<Renderer>().enabled = false;
            markerShadow.GetComponent<Renderer>().enabled = false;
            markerButton.GetComponent<Renderer>().enabled = false;
            footprint.gameObject.GetComponent<Renderer>().material.color =
                new Color(1, 1, 1, 0.25f);
            footprint.gameObject.GetComponent<Renderer>().material.mainTexture =
                opponentTexture;
        }

        IsReady = false;
        gameManager.ShowMessage("Drag your game marker to a good position", 7);
	}

    private void Start() {
        isVisible = true;

        if (isPlayer) {
            transform.position = GetFloorPos(
                new Vector2(Screen.width / 2, Screen.height / 2)
            );

            transform.localPosition += 
                new Vector3(MultiplayerContol.Instance.IsHost ? 0.5f : -0.5f, 0, 0);
        }

        GameManager.Instance.UpdateIslandSettings(
            playerName,
            transform.localPosition,
            transform.localRotation,
            false,
            false);
    }


	void Update () {

        if (!isVisible)
            return;

        if (!isComplete && visibleAmount <= 1.0f) {
            visibleAmount += Time.deltaTime / 0.5f;
            float amount = Mathf.SmoothStep(0, 1, visibleAmount);
            transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one, amount);
        }

        if (isComplete) {
            visibleAmount -= Time.deltaTime / 0.5f;
            float amount = Mathf.SmoothStep(0, 1, visibleAmount);
            transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one, amount);
            if (visibleAmount < 0.1f) {
                Destroy(gameObject);
            }
            return;
        }

        if (isPlayer) {
            updateTimer -= Time.deltaTime;
            if (updateTimer < 0) {
                updateTimer = 1.0f;
                PostSettings();
            }
        }
       

        if (!opponentPlacer) {
            GameObject[] placers = GameObject.FindGameObjectsWithTag("IslandPlacer");
            foreach (var placer in placers) {
                if (!placer.Equals(gameObject)) {
                    opponentPlacer = placer.GetComponent<IslandPlacer>();
                }
            }
        }

        if (opponentPlacer) {
            float distance = Vector3.Distance(transform.position, opponentPlacer.transform.position);
            if (distance > 0.1f) {
                transform.LookAt(opponentPlacer.transform, transform.parent.up);
                Vector3 opponentPos = opponentPlacer.transform.position;
                opponentPos.y = 0;
                Vector3 myPos = transform.position;
                myPos.y = 0;
                transform.rotation = Quaternion.LookRotation(opponentPos - myPos);               
            }

            TooClose = distance < minDistance;
            if (isPlayer && isReady && opponentPlacer.IsReady) {
                Place();
            }
        }

        messageBox.transform.rotation = Quaternion.LookRotation(messageBox.transform.position - Camera.main.transform.position);

        if (messageTimer > 0) {
            messageTimer -= Time.deltaTime;
            if (messageTimer < 0) {
                messageBox.SetActive(false);
            }
        }
	}

    void PostSettings() {
        GameManager.Instance.UpdateIslandSettings(
            playerName,
            transform.localPosition,
            transform.localRotation,
            isReady,
            false
        );
    }

    public void OnPointerDown(PointerEventData eventData) {
        dragOffset = transform.position - GetFloorPos(Input.mousePosition);
        IsDragging = true;
		audioSource.PlayOneShot(islandPlacerDownSound);
    }

    public void OnPointerDrag(PointerEventData eventData) {
        Vector3 newPos = GetFloorPos(Input.mousePosition) + dragOffset;
        transform.position = newPos;
        IsReady = false;
    }

    public void OnPointerUp(PointerEventData eventData) {
        IsDragging = false;
		audioSource.PlayOneShot(islandPlacerUpSound);
    }


    public void GetNetworkUpdate(object sender2, ValueChangedEventArgs data) {
        if (isComplete)
            return;
        
        if (data.Snapshot != null) {
            isVisible = true;

            if (!data.Snapshot.Child("localPosition").Exists) return;
            if (!data.Snapshot.Child("localRotation").Exists) return;

            string posString = data.Snapshot.Child("localPosition").Value.ToString();
            string[] posArray = posString.Split(',');
            Vector3 pos = new Vector3(
            float.Parse(posArray[0]),
            float.Parse(posArray[1]),
            float.Parse(posArray[2]));

            string rotString = data.Snapshot.Child("localRotation").Value.ToString();
            string[] rotArray = rotString.Split(',');
            Quaternion rot = Quaternion.Euler(
            float.Parse(rotArray[0]),
            float.Parse(rotArray[1]),
            float.Parse(rotArray[2]));

            transform.localPosition = pos;
            transform.localRotation = rot;

            if ((bool)data.Snapshot.Child("placed").Value) {
                Debug.Log("Placing Island for playerName");
                gameManager.PlaceIsland(playerName, transform.localPosition, transform.localRotation);
                isComplete = true;
            }
            isReady = (bool)data.Snapshot.Child("ready").Value;

        }

    }

    public void ButtonClicked(PointerEventData eventData) {
        if (!isVisible) return;

        audioSource.PlayOneShot(clickSound);

        if (!opponentPlacer) {
            ShowMessage("You need an opponent to join before you can start!", 5);
            return;
        }

        if (tooClose) {
            ShowMessage("Your marker is to too close to your opponent!", 5);
            return;
        }

        IsReady = true;
        ShowMessage("You're ready, just waiting for your opponent", 5);
    }

    public void Place() {
        PostSettings();
        audioSource.PlayOneShot(placeSound);
        isComplete = true;        
        Debug.Log("StartGame");
        gameManager.PlaceIsland(transform.localPosition, transform.localRotation);
    }

    Vector3 GetFloorPos(Vector3 screenPos) {
        Ray screenRay = Camera.main.ScreenPointToRay(screenPos);
        Plane floor = new Plane(transform.root.up, transform.root.position);
        float distance = 0;
        Vector3 touchPos = Vector3.zero;
        if (floor.Raycast(screenRay, out distance)) {
            touchPos = screenRay.GetPoint(distance);
        }
        return touchPos;
    }

    void OnDestroy() {

        Debug.Log("Removing listeners for " + gameObject.name);
        if (isPlayer) {
            footprintInput.onPointerDown.RemoveListener(OnPointerDown);
            footprintInput.onPointerUp.RemoveListener(OnPointerUp);
        } else {
            MultiplayerContol.Instance.RemoveIslandListener(playerName, GetNetworkUpdate);
        }
    }

    void ShowMessage(string message, float time) {
        messageText.text = message;
        messageTimer = time;
        messageBox.SetActive(true);
    }
}
