//-----------------------------------------------------------------------
// <copyright file="TileDebris.cs" company="Google">
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


using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TileDebris : MonoBehaviour, IPointerClickHandler { 

    Transform glow;

    Vector3 rotAmount;

    Vector3 homePos;
    Vector3 floatingPos;

    float moveTimer;
    float moveDuration = 1.0f;
    public AnimationCurve moveCurve;

    Renderer targetTile;

    int ownerNumber;

    bool isReturning = false;
    public bool IsReturning {
        get { return isReturning; }
        set {
            isReturning = value;
            moveTimer = Mathf.Clamp01(moveTimer);
        }
    }

    AudioSource audioSource;
    public AudioClip createSound;
    public AudioClip clickSound;
    public AudioClip tileReturnSound;

    ParticleSystem tapEffect;


    public void Init(Vector3 pos, Renderer tileRenderer, Vector3 offset, string debrisName, int projectilePlayer) {
        targetTile = tileRenderer;
        rotAmount = new Vector3(
            Random.Range(90, 90), 
            Random.Range(0, 180), 
            Random.Range(0, 180));

        glow = transform.Find("Glow");
        homePos = pos;
        floatingPos = homePos + offset;
        gameObject.name = debrisName;

        ownerNumber = projectilePlayer == 1 ? 2 : 1;
   
        Material[] newMats = GameManager.Instance.GetPlayerMaterials(ownerNumber);
        GetComponent<Renderer>().materials = newMats;
        MultiplayerContol.Instance.AddDebrisListener(debrisName, GetNetworkUpdate);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(createSound);

        tapEffect = transform.Find("TapTilePulse").GetComponent<ParticleSystem>();
    }

    void Update () {
        transform.Rotate(rotAmount * Time.deltaTime);
        glow.LookAt(Camera.main.transform);

        if (moveTimer >= 0.0f && moveTimer <= 1.0f) {
            moveTimer += Time.deltaTime / (IsReturning ? -moveDuration : moveDuration);
            float amount = moveCurve.Evaluate(moveTimer);
            transform.localPosition = Vector3.Lerp(homePos, floatingPos, amount);

            Material[] newMats = GameManager.Instance.GetPlayerMaterials(0);

            if (IsReturning && moveTimer < 0) {
                if (targetTile) {
                    targetTile.gameObject.name = "tile-Recovered";
                    targetTile.materials = newMats;
                    GameManager.Instance.PlayClip(tileReturnSound);
                    GameObject sparks = Instantiate(
                        GameManager.Instance.sparksPrefabs[ownerNumber - 1],
                        homePos,
                        Quaternion.identity,
                        GameManager.Instance.WorldAnchor);

                    sparks.transform.localPosition = homePos;

                    Destroy(gameObject);
                }
            }
        }
    }

    public void GetNetworkUpdate(object sender2, ValueChangedEventArgs data) {
        if (IsReturning)
            return;

        if (data.Snapshot != null) {
            if (data.Snapshot.Value.ToString().Equals("cleared")) {
                Debug.Log("Removing Debris");
                IsReturning = true;
                audioSource.PlayOneShot(clickSound);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        int playerNumber = MultiplayerContol.Instance.IsHost ? 1 : 2;

        if (playerNumber == ownerNumber) return;


        tapEffect.Play();
        IsReturning = true;
        audioSource.PlayOneShot(clickSound);
        MultiplayerContol.Instance.ClearDebris(gameObject.name);
    }

    private void OnDestroy() {
        MultiplayerContol.Instance.RemoveDebrisListener(gameObject.name, GetNetworkUpdate);
    }
}
