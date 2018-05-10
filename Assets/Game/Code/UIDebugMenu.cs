//-----------------------------------------------------------------------
// <copyright file="UIDebugMenu.cs" company="Google">
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

public class UIDebugMenu : MonoBehaviour {

    GameObject menuContainer;
    bool redLobbyAvailable = true;
    Button redLobbyButton;
    Color redLobbyColor;
    bool greenLobbyAvailable = true;
    Button greenLobbyButton;
    Color greenLobbyColor;
    bool blueLobbyAvailable = true;
    Button blueLobbyButton;
    Color blueLobbyColor;

    RectTransform rectTransform;
    Vector2 upPos = new Vector2(0, 0);
    Vector2 downPos = new Vector2(0, 0);

    bool isVisible = false;
    float visibleTimer = 0;
    float visibleDuration = 0.25f;
    public bool IsVisible {
        get { return isVisible; }
        set {
            isVisible = value;
            menuContainer.SetActive(true);
            visibleTimer = Mathf.Clamp01(visibleTimer);
        }
    }

    Button closeButton;


    void Start () {

        rectTransform = GetComponent<RectTransform>();
        upPos = new Vector2(0, rectTransform.rect.height);
        rectTransform.anchoredPosition = upPos;
        menuContainer = transform.Find("MenuContainer").gameObject;
        redLobbyButton = transform.Find("MenuContainer/RedLobby").GetComponent<Button>();
        redLobbyButton.onClick.AddListener(ToggleRedLobby);
        redLobbyColor = transform.Find("MenuContainer/RedLobby").GetComponent<Image>().color;

        greenLobbyButton = transform.Find("MenuContainer/GreenLobby").GetComponent<Button>();
        greenLobbyButton.onClick.AddListener(ToggleGreenLobby);
        greenLobbyColor = transform.Find("MenuContainer/GreenLobby").GetComponent<Image>().color;

        blueLobbyButton = transform.Find("MenuContainer/BlueLobby").GetComponent<Button>();
        blueLobbyButton.onClick.AddListener(ToggleBlueLobby);
        blueLobbyColor = transform.Find("MenuContainer/BlueLobby").GetComponent<Image>().color;

        closeButton = transform.Find("MenuContainer/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

        SetButtonStatus();
    }

    void Update () {
        if (visibleTimer >= 0.0f && visibleTimer <= 1.0f) {
            visibleTimer += Time.deltaTime / (isVisible ? visibleDuration : -visibleDuration);
            float amount = Mathf.SmoothStep(0, 1, visibleTimer);
            rectTransform.anchoredPosition = Vector2.Lerp(upPos, downPos, amount);
            if (!isVisible && visibleTimer < 0) {
                menuContainer.SetActive(false);
            }
        }


        if (Input.touchCount >= 3 || Input.GetKeyDown(KeyCode.X)) {
            Open();
        }
    }

    void Toggle() {
        IsVisible = !IsVisible;
    }

    void Open() {
        IsVisible = true;
    }

    void Close() {
        IsVisible = false;
    }

    void ToggleRedLobby() {
        redLobbyAvailable = !redLobbyAvailable;
        Color redColor = redLobbyAvailable ? redLobbyColor : redLobbyColor * 0.25f;
        redLobbyButton.transform.GetComponent<Image>().color = redColor;
        PlayerPrefs.SetString("redLobbyAvailable", redLobbyAvailable ? "on" : "off");
        MultiplayerContol.Instance.ClearRoom(0);

    }

    void ToggleGreenLobby() {
        greenLobbyAvailable = !greenLobbyAvailable;
        Color greenColor = greenLobbyAvailable ? greenLobbyColor : greenLobbyColor * 0.25f;
        greenLobbyButton.transform.GetComponent<Image>().color = greenColor;
        PlayerPrefs.SetString("greenLobbyAvailable", greenLobbyAvailable ? "on" : "off");
        MultiplayerContol.Instance.ClearRoom(1);
    }

    void ToggleBlueLobby() {
        blueLobbyAvailable = !blueLobbyAvailable;
        Color blueColor = blueLobbyAvailable ? blueLobbyColor : blueLobbyColor * 0.25f;
        blueLobbyButton.transform.GetComponent<Image>().color = blueColor;
        PlayerPrefs.SetString("blueLobbyAvailable", blueLobbyAvailable ? "on" : "off");
        MultiplayerContol.Instance.ClearRoom(2);
    }

    void SetButtonStatus() {
        string redStatus = PlayerPrefs.GetString("redLobbyAvailable");
        redLobbyAvailable = !redStatus.Equals("off");

        string greenStatus = PlayerPrefs.GetString("greenLobbyAvailable");
        greenLobbyAvailable = !greenStatus.Equals("off");

        string blueStatus = PlayerPrefs.GetString("blueLobbyAvailable");
        blueLobbyAvailable = !blueStatus.Equals("off");
    }

    public void ResetGame() {
        Application.Quit();
    }
}
