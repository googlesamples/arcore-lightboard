//-----------------------------------------------------------------------
// <copyright file="ParticleKiller.cs" company="Google">
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

public class ParticleKiller : MonoBehaviour {

    ParticleSystem system;
    ParticleSystem[] allSystems;

    float waitTime = 1f;

	void Start () {
        system = GetComponent<ParticleSystem>();
        allSystems = GetComponentsInChildren<ParticleSystem>();
	}
	
	void Update () {
        if (waitTime > 0) {
            waitTime -= Time.deltaTime;
            if (waitTime < 0) {
                foreach(ParticleSystem pSystem in allSystems) {
                    ParticleSystem.MainModule main = pSystem.main;
                    main.loop = false;
                }
            }
        }

        if (waitTime < 0 && !system.IsAlive()) 
            Destroy(gameObject);
	}
}
