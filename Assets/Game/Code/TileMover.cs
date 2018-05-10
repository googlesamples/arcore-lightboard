//-----------------------------------------------------------------------
// <copyright file="TileMover.cs" company="Google">
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

public class TileMover : MonoBehaviour {

    AnimationCurve animCurve;

    float timer = 0.0f;
    float duration = 0.5f;
    float travel = 0.0f;

    int totalFrames = 9;

    Material[] nextMats;

    Renderer lightBeamRenderer;
    Renderer tileFlashRenderer;

    Vector3 homePos;


    public void Init(float distance, AnimationCurve newAnimCurve, Material[] newMats) {
        animCurve = newAnimCurve;
        timer = (-distance * 0.5f) / duration;
        travel = (1 - distance) * 0.005f;
        nextMats = newMats;

        homePos = transform.localPosition;

        foreach(Transform child in transform) {
            if (child.name.Equals("Flash")) {
                tileFlashRenderer = child.GetComponent<Renderer>();
                child.gameObject.SetActive(true);
            }
            if (child.name.Equals("Light")) {
                Vector3 lightScale = child.localScale;
                lightScale.y = (1 - distance) + 1.5f;
                child.localScale = lightScale;
                lightBeamRenderer = child.GetComponent<Renderer>();
                child.gameObject.SetActive(true);
            }
        }
    }

    void Update () {
        timer += Time.deltaTime / duration;
        float amount = animCurve.Evaluate(timer);
        int frameNumber = Mathf.CeilToInt(timer * totalFrames);
        tileFlashRenderer.material.SetInt("_Frame", Mathf.Max(frameNumber, 0));
        if (frameNumber > 0) {
            GetComponent<Renderer>().materials = nextMats;
        }

        Vector3 localPos = homePos;
        localPos.y += amount * travel;
        transform.localPosition = localPos;

        Color lightColor = lightBeamRenderer.material.GetColor("_Color");
        lightColor.a = amount;
        lightBeamRenderer.material.SetColor("_Color", lightColor);

        if (timer > 1.0f) {
            lightBeamRenderer.gameObject.SetActive(false);
            tileFlashRenderer.gameObject.SetActive(false);
            Destroy(this);
        }

    }
}
