//-----------------------------------------------------------------------
// <copyright file="UITipsPage.cs" company="Google">
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

public class UITipsPage : MonoBehaviour {

    float timer = 0.0f;
    float duration = 0.5f;
    bool isVisible = false;
    public bool IsVisible {
        set {
            isVisible = value;
            timer = Mathf.Clamp01(timer);
            gameObject.SetActive(true);
        }
    }

    CanvasGroup canvasGroup;

    RectTransform rectTransform;
    public RectTransform RectTransform {
        get { return rectTransform; }
    }

    void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup.alpha = 0;
        IsVisible = false;
    }
	
	void Update () {
        if (timer >= 0.0f && timer <= 1.0f) {
            timer += Time.deltaTime / (isVisible ? duration : -duration);
            float amount = Mathf.SmoothStep(0, 1, timer);
            canvasGroup.alpha = amount;
            if (timer < 0.0f) {
                gameObject.SetActive(false);
            }
        }
    }
}
