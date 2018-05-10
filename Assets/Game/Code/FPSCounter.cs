//-----------------------------------------------------------------------
// <copyright file="FPSCounter.cs" company="Google">
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
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    Text fpsText;

    void Start() {
        fpsText = GetComponent<Text>();
        StartCoroutine(updateFPSText());
    }

    IEnumerator updateFPSText() {
        while (true)
        {
            if (fpsText != null)
            {
                fpsText.text = string.Format("FPS: {0} / {1}ms", (1.0f / Time.smoothDeltaTime).ToString("0.0"), (Time.smoothDeltaTime * 1000.0f).ToString("0"));
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

}
