//-----------------------------------------------------------------------
// <copyright file="UIIntro.cs" company="Google">
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

public class UIIntro : MonoBehaviour {

    RectTransform rectTransform;
    Vector2 openPos = new Vector2(0, 0);
    Vector2 closePos = new Vector2(0, 0);
    RectTransform background;
    Button closeButton;

    UILobby lobbyUI;

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

    void Start() {
        rectTransform = GetComponent<RectTransform>();

        background = transform.Find("Background").GetComponent<RectTransform>();
        openPos = background.anchoredPosition;
        closePos = openPos - new Vector2(0.0f, rectTransform.rect.height);
        closeButton = transform.Find("Background/OKButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnClick);
        lobbyUI = transform.parent.Find("LobbyUI").GetComponent<UILobby>();
        IsVisible = true;
    }

    void Update() {
        if (visibleTimer >= 0.0f && visibleTimer <= 1.0f) {
            visibleTimer += Time.deltaTime / (isVisible ? visibleDuration : -visibleDuration);
            float amount = Mathf.SmoothStep(0, 1, visibleTimer);
            background.anchoredPosition = Vector2.Lerp(closePos, openPos, amount);
        }
    }

    public void OnClick() {
        IsVisible = false;
        lobbyUI.Open();
    }
}
