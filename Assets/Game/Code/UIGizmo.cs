//-----------------------------------------------------------------------
// <copyright file="UIGizmo.cs" company="Google">
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

public class UIGizmo : MonoBehaviour {

    Material UIMat;

    float timer = 0.0f;
    float duration = 0.5f;

    Color openColor;
    Color closedColor;

    bool uiVisible = false;
    public bool UIVisible {
        get { return uiVisible; }
        set {
            if (uiVisible == value)
                return;
            
            uiVisible = value;
            timer = Mathf.Clamp01(timer);
            if (uiVisible) gameObject.SetActive(true);
        }
    }


	void Start () {
        UIMat = Instantiate(gameObject.GetComponent<Renderer>().material);
        openColor = UIMat.color;
        closedColor = openColor;
        closedColor.a = 0.0f;
        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends) {
            rend.material = UIMat;
        }
	}
	
	void Update () {
        if (timer >= 0.0f && timer <= 1.0f) {
            timer += Time.deltaTime / (uiVisible ? duration : -duration);
            float amount = Mathf.SmoothStep(0, 1, timer);
            UIMat.color = Color.Lerp(closedColor, openColor, amount);
            if (timer < 0.0f) {
                gameObject.SetActive(false);
            }
        }
	}
}
