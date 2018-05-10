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
using UnityEngine;

public class IslandFootprint : MonoBehaviour {



    bool isReady = false;

	public bool IsReady {
        get { return isReady; }

        set {
            isReady = value;
            colorTimer = Mathf.Clamp01(colorTimer);
        }
    }

    float colorTimer = 0.0f;
    float colorDuration = 0.25f;

    Renderer rend;

    void Start () {
        rend = GetComponent<Renderer>();

        Vector3 foorprintSize = GameManager.Instance.GridSize;
        foorprintSize.y = 1.0f;
        transform.localScale = foorprintSize;
        GetComponent<Renderer>().material.renderQueue -= 1;
	}
	
	void Update () {
        if (colorTimer >= 0.0f && colorTimer <= 1.0f) {
            colorTimer += Time.deltaTime / (isReady ? colorDuration : -colorDuration);
            float amount = Mathf.SmoothStep(0, 1, colorTimer);
            rend.material.color = Color.Lerp(Color.red, Color.white, amount);
        }
	}
}
