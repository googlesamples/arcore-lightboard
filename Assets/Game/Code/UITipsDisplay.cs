//-----------------------------------------------------------------------
// <copyright file="UITipsDisplay.cs" company="Google">
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

public class UITipsDisplay : MonoBehaviour {



    UITipsPage[] pages;
    int pageIndex;

    Text label;

    float pageTimer = 1.0f;
    float pageDuration = 3.0f;

    bool isVisible = false;
    float visibleTimer = 0;
    float visibleDuration = 0.25f;
    public bool IsVisible {
        get { return isVisible; }
        set {
            isVisible = value;
            Debug.Log(isVisible ? "Opening Tips" : "Closing Tips");
            gameObject.SetActive(true);
            visibleTimer = Mathf.Clamp01(visibleTimer);
        }
    }

    RectTransform rectTransform;
    Vector2 upPos = new Vector2(0, 400);
    Vector2 downPos = new Vector2(0, 0);

    bool switching = false;
    float switchTimer = 0.0f;
    float switchDuration = 0.5f;
    UITipsPage oldPage;
    UITipsPage currentPage;

    Vector2 pageLeft = new Vector2(-500, 30);
    Vector2 pageCenter = new Vector2(0, 30);
    Vector2 pageRight = new Vector2(500, 30);


    void Start () {
        rectTransform = GetComponent<RectTransform>();
        upPos = new Vector2(0, rectTransform.rect.height);

        pages = GetComponentsInChildren<UITipsPage>();
        label = transform.Find("TipLabel").GetComponent<Text>();
        label.text = "";


        IsVisible = false;
    }

    void Update () {

        if (visibleTimer >= 0.0f && visibleTimer <= 1.0f) {
            visibleTimer += Time.deltaTime / (isVisible ? visibleDuration : -visibleDuration);
            float amount = Mathf.SmoothStep(0, 1, visibleTimer);
            rectTransform.anchoredPosition = Vector2.Lerp(upPos, downPos, amount);
            if (!isVisible && visibleTimer < 0) {
                gameObject.SetActive(false);
            }
        }

        if (isVisible) {
            pageTimer -= Time.deltaTime;
            if (pageTimer < 0)
                SwitchPage();
        }

        if(switching) {
            switchTimer += Time.deltaTime / switchDuration;
            float amount = Mathf.SmoothStep(0, 1, switchTimer);
            if (oldPage) {
                oldPage.RectTransform.anchoredPosition = Vector2.Lerp(pageCenter, pageLeft, amount);
                oldPage.IsVisible = false;
            }
            currentPage.RectTransform.anchoredPosition = Vector2.Lerp(pageRight, pageCenter, amount);
            currentPage.IsVisible = true;
            if (switchTimer > 1)
                switching = false;
        }
	}

    void SwitchPage() {
        pageTimer = pageDuration;
        switching = true;
        switchTimer = 0;
        oldPage = currentPage;
        currentPage = pages[pageIndex];
        label.text = string.Format("TIP {0} of {1}", pageIndex + 1, pages.Length);
        
        pageIndex = (pageIndex+1) % pages.Length;
    }
}
