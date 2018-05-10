//-----------------------------------------------------------------------
// <copyright file="TrajectoryIndicator.cs" company="Google">
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

public class TrajectoryIndicator : MonoBehaviour {

    LineRenderer trajectoryLine;

    LauncherControl launcherControl;

    public LauncherControl LauncherControl {
        set {
            Debug.Log("Setting control to " + value.name);
            launcherControl = value; 
        }
    }

    Transform launchPoint;
    public Transform LaunchPoint {
        set { launchPoint = value; }
    }

    float amount = 1;
    Color lineClear = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    Color lineWhite = new Color(1.0f, 1.0f, 1.0f, 0.25f);

    float fadeTimer = 0.0f;
    float fadeDuration = 0.1f;
    bool isReady = false;
    public bool IsReady {
        get { return isReady; }
        set {
            if (isReady == value) return;
            fadeTimer = Mathf.Clamp01(fadeTimer);
            isReady = value;
        }
    }

    void Start () {
        trajectoryLine = GetComponent<LineRenderer>();
    }

    void Update () {
        if (isReady) {
            DrawTrajectory();
        }

        if (fadeTimer >= 0.0f && fadeTimer <= 1.0f) {
            fadeTimer += Time.deltaTime / (isReady ? fadeDuration : -fadeDuration);
            float fadeAmount = Mathf.SmoothStep(0, 1, fadeTimer);
            Color lineColor = Color.Lerp(lineClear, lineWhite, fadeAmount);
            trajectoryLine.startColor = trajectoryLine.endColor = lineColor;
        }
    }

    void DrawTrajectory() {
        for (var i = 0; i < trajectoryLine.positionCount; i++) {
            trajectoryLine.SetPosition(i, EvaluateTrajectory(i * (amount / trajectoryLine.positionCount)));
        }
    }

    public Vector3 EvaluateTrajectory(float time) {
        Vector3 velocity = launchPoint.forward * launcherControl.TotalPower;
        Vector3 relativeVel =
            GameManager.Instance.WorldAnchor.InverseTransformDirection(velocity);
        
        Vector3 pos = GameManager.Instance.WorldAnchor.InverseTransformPoint(launchPoint.position);;
        pos += (relativeVel * time) + (GameManager.Instance.Gravity * time * time);
        return pos;
    }
}
