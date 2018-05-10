//-----------------------------------------------------------------------
// <copyright file="ProgressControl.cs" company="Google">
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

public class ProgressControl : MonoBehaviour {

    private bool loadingComplete = false;

    float loadingTimer = 0.0f;
    float loadingCompleteTimer = 0.0f;

    Image loadingSpinner;
    float spinnerRotation;
    Text loadingText;
    string message = "";

    void Start () {
        loadingText = transform.Find("ProgressLabel").GetComponent<Text>();
        Close();
    }

    void Update () {

        loadingTimer += Time.deltaTime;
        float fadeAmount = Mathf.SmoothStep(0, 1, loadingTimer - 1);
        spinnerRotation += Time.deltaTime;
        loadingText.color = Color.white * fadeAmount;

        int dotsAnim = Mathf.FloorToInt(loadingTimer * 5) % 4;
        string dots = "";
        switch (dotsAnim) {
            case 0:
                dots = "   ";
                break;
            case 1:
                dots = ".  ";
                break;
            case 2:
                dots = " . ";
                break;
            case 3:
                dots = "  .";
                break;
            default:
                dots = "";
                break;
        }
        loadingText.text = message + dots;
            
        if (loadingComplete) {
            loadingCompleteTimer += Time.deltaTime * 3;
            float amount = Mathf.SmoothStep(0, 1, 1 - loadingCompleteTimer);
            loadingText.color = Color.white * amount;
            if (loadingCompleteTimer > 1.0f)
                FinishClosing();
        }
    }

    public void Open() {
        loadingTimer = 0;
        loadingComplete = false;
        gameObject.SetActive(true);

        if (MultiplayerContol.Instance.IsHost) {
            SetText("Hosting Game");
        } else {
            SetText("Joining Game");
        }
    }

    public void Close() {
        loadingComplete = true;
    }

    public void FinishClosing() {
        gameObject.SetActive(false);

    }


    public void SetText(string newText) {
        if (!loadingText)
            loadingText = transform.Find("LoadingText").GetComponent<Text>();
        message = newText;
        loadingText.text = newText;
    }
}
