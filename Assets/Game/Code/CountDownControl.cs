//-----------------------------------------------------------------------
// <copyright file="CountDownControl.cs" company="Google">
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


using UnityEngine;
using UnityEngine.UI;

public class CountDownControl : MonoBehaviour {

    Text bigNumber;

    UIControl uiControl;
    RectTransform rectTransform;

    Vector2 openSize = new Vector2(400, 400);
    Vector2 closedSize = new Vector2(50, 50);

    CanvasGroup canvasGroup;

    float startTimer = 5.5f;

    bool isVisible = false;
    float visibleTimer = 0;
    float visibleDuration = 0.25f;
    public bool IsVisible {
        get { return isVisible; }
        set {
            isVisible = value;
            visibleTimer = Mathf.Clamp01(visibleTimer);
            if (isVisible) {
                gameObject.SetActive(true);
                startTimer = 5.5f;
            }
        }
    }

    int lastBigNumber = 0;

    public void Init(UIControl control) {
        uiControl = control;
        rectTransform = GetComponent<RectTransform>();
        bigNumber = transform.Find("Number").GetComponent<Text>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
	}
	
	void Update () {
        if (visibleTimer >= 0.0f && visibleTimer <= 1.0f) {
            visibleTimer += Time.deltaTime / (isVisible ? visibleDuration : -visibleDuration);
            float amount = Mathf.SmoothStep(0, 1, visibleTimer);
            rectTransform.sizeDelta = Vector2.Lerp(closedSize, openSize, amount);
            canvasGroup.alpha = amount;
            if (!isVisible && visibleTimer < 0) {
                gameObject.SetActive(false);
            }
        }

        if (startTimer > 0 && visibleTimer >= 1) {
            startTimer -= Time.deltaTime;
            if (startTimer < 0) {
                IsVisible = false;
                uiControl.PlayCountDownFinished();
            }

            startTimer = Mathf.Clamp(startTimer, 0, 5);
            int number = Mathf.FloorToInt(startTimer);
            if (number != lastBigNumber) {
                uiControl.PlayClick(number);
            }

            lastBigNumber = number;
            bigNumber.text = number.ToString();
        }
	}
}
