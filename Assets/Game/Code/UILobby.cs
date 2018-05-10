//-----------------------------------------------------------------------
// <copyright file="UILobby.cs" company="Google">
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

public class UILobby : MonoBehaviour {

    public Sprite iconFull;
    public Sprite iconEmpty;
    public Sprite iconJoin;
    public Sprite[] iconLoading;

    RoomConfig redLobbyButton;
    RoomConfig greenLobbyButton;
    RoomConfig blueLobbyButton;

    RectTransform rectTransform;
    RectTransform lobbyHeader;
    Vector2 upPos = new Vector2(0,100);
    Vector2 downPos = new Vector2(0,0);
    UIControl uiControl;

    bool isVisible = false;
    float visibleTimer = 0;
    float visibleDuration = 0.25f;
    public bool IsVisible {
        get { return isVisible; }
        set {
            isVisible = value;
            visibleTimer = Mathf.Clamp01(visibleTimer);
        }
    }

	void Start () {
        uiControl = transform.root.GetComponent<UIControl>();
        rectTransform = GetComponent<RectTransform>();
        redLobbyButton = transform.Find("LobbyButtons/Lobby-0").GetComponent<RoomConfig>();
        greenLobbyButton = transform.Find("LobbyButtons/Lobby-1").GetComponent<RoomConfig>();
        blueLobbyButton = transform.Find("LobbyButtons/Lobby-2").GetComponent<RoomConfig>();
        lobbyHeader = transform.Find("LobbyHeader").GetComponent<RectTransform>();

        rectTransform.anchoredPosition = downPos;
        IsVisible = false;
    }

    void Update () {
        if (visibleTimer >= 0.0f && visibleTimer <= 1.0f) {
            visibleTimer += Time.deltaTime / (isVisible ? visibleDuration : -visibleDuration);
            float amount = Mathf.SmoothStep(0, 1, visibleTimer);
            rectTransform.anchoredPosition = Vector2.Lerp(downPos, upPos, amount);
            if (!isVisible && visibleTimer < 0) {
                redLobbyButton.IsActive = false;
                greenLobbyButton.IsActive = false;
                blueLobbyButton.IsActive = false;
            }
        }
	}

    public void Open() {
        StartCoroutine(RevealButtons());
    }

    public void Close() {
        IsVisible = false;
    }

    public void RefreshButtons() {
        redLobbyButton.Reset();
        greenLobbyButton.Reset();
        blueLobbyButton.Reset();
        StartCoroutine(RevealButtons());
    }

    IEnumerator RevealButtons() {
        yield return new WaitForSeconds(1.0f);
        int numberOfLobbies = 0;
        if (!PlayerPrefs.GetString("redLobbyAvailable").Equals("off")) {
            redLobbyButton.Init(this);
            redLobbyButton.IsActive = true;
            numberOfLobbies++;
            yield return new WaitForSeconds(0.1f);
        } else {
            redLobbyButton.gameObject.SetActive(false);
        }

        if (!PlayerPrefs.GetString("greenLobbyAvailable").Equals("off")) {
            greenLobbyButton.Init(this);
            greenLobbyButton.IsActive = true;
            numberOfLobbies++;
            yield return new WaitForSeconds(0.1f);
        } else {
            greenLobbyButton.gameObject.SetActive(false);
        }

        if (!PlayerPrefs.GetString("blueLobbyAvailable").Equals("off")) {
            blueLobbyButton.Init(this);
            blueLobbyButton.IsActive = true;
            numberOfLobbies++;
            yield return new WaitForSeconds(0.1f);
        } else {
            blueLobbyButton.gameObject.SetActive(false);
        }
        uiControl.CloseIntro();

        upPos = new Vector2(0, lobbyHeader.rect.height + (numberOfLobbies * 260) + 100);
        IsVisible = true;
    }
}
