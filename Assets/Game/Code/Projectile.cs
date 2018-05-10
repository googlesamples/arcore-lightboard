//-----------------------------------------------------------------------
// <copyright file="Projectile.cs" company="Google">
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

public class Projectile : MonoBehaviour {

    GameManager gameManager;

    Island world;
    SphereCollider projectileCollider;

    Vector3 randomRot;

    float maxTime = 3.0f;
    float blastRadius = 0.18f;

    Vector3 velocity = Vector3.zero;

    LauncherControl owner;
    LauncherShot launcherShot;
    GameObject glow;
    TrailRenderer trail;
    AudioSource audioSource;
    public AudioClip preImpactSound;
    bool preImpactPlayed = false;
    public AudioClip impactBoardSound;
	public AudioClip impactGroundSound;
    public AudioClip[] tilePainted;
    bool impactPlayed = false;

    float travelTimer = 0.0f;
    Vector3 startPostion = Vector3.zero;

    public Material firingLineMat;
    public AnimationCurve tileHitCurve;
    public GameObject floorQuadPrefab;

    float deathTimer = -1.0f;

    public void Launch(LauncherShot newLauncherShot) {
        launcherShot = newLauncherShot;

        projectileCollider = GetComponent<SphereCollider>();
        projectileCollider.enabled = true;
        gameManager = GameManager.Instance;
        randomRot = new Vector3(Random.value * 40, Random.value * 1, Random.value * 1);
        velocity = launcherShot.Velocity;
        startPostion = launcherShot.StartPos;
        audioSource = gameObject.AddComponent<AudioSource>();

        if (!launcherShot.IsReplay) {
            float timeToImpact;
            Vector3 impactPos;
            launcherShot.DoesHit = SimulateTrajectory(startPostion, velocity, out timeToImpact, out impactPos, false);
            launcherShot.TimeToImpact = timeToImpact;
            launcherShot.ImpactPos = impactPos;           
        }

        glow = Instantiate(gameManager.projectileGlowPrefab);
        glow.GetComponent<ProjectileGlow>().Init(transform);
        trail = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate() {

        if (deathTimer > 0) {
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0)
                Destroy(gameObject);

            return;
        }

        travelTimer += Time.deltaTime;
        transform.localPosition = launcherShot.Evaluate(travelTimer);
        trail.material.mainTextureOffset += new Vector2(0.1f * Time.deltaTime, 0);

        if (!preImpactPlayed && launcherShot.BoardHit && travelTimer > (launcherShot.TimeToImpact * 0.9f)) {
            audioSource.PlayOneShot(preImpactSound);
            preImpactPlayed = true;
        }
 
        if (travelTimer > Mathf.Min(launcherShot.TimeToImpact, maxTime)) {
            if (launcherShot.DoesHit) {
               
                if (launcherShot.BoardHit) {
                    HitBoard();
					if (!impactPlayed) {
						audioSource.PlayOneShot(impactBoardSound);
						impactPlayed = true;
					}
                } else {
                    HitGround();
					if (!impactPlayed) {
						audioSource.PlayOneShot(impactGroundSound);
						impactPlayed = true;
					}
                }
            }
            Destroy(glow);
        }
        transform.Rotate(randomRot);
    }

    void HitBoard() {
        Debug.Log("Hitting ground");

        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tiles");
        Renderer someTileRenderer = null;
        foreach (GameObject tile in allTiles) {
            Vector3 tilePos = gameManager.WorldAnchor.InverseTransformPoint(tile.transform.position);
            float distance = Vector3.Distance(tilePos, launcherShot.ImpactPos) / blastRadius;
            if (distance < 1.0f) {

                Material[] newMats = gameManager.GetPlayerMaterials(
                    launcherShot.Owner.PlayerNumber);

                tile.AddComponent<TileMover>().Init(distance, tileHitCurve, newMats);

                if (!tile.name.Contains("Painted")) {
                    tile.name = "Tile-Painted";
                    if (someTileRenderer == null) {
                        someTileRenderer = tile.GetComponent<Renderer>();
                    }
                }
            }
        }

        if (launcherShot.HasDebris && someTileRenderer) {
            Vector3 debrisPos = gameManager.WorldAnchor.InverseTransformPoint(someTileRenderer.transform.position);
            gameManager.AddDebris(debrisPos, 
                launcherShot.DebrisOffset, 
                someTileRenderer, 
                launcherShot.DebrisName, 
                launcherShot.Owner.PlayerNumber);
        }

        GameObject sparks = Instantiate(
            gameManager.sparksPrefabs[launcherShot.Owner.PlayerNumber - 1],
            launcherShot.ImpactPos,
            Quaternion.identity,
            gameManager.WorldAnchor);

        sparks.transform.localPosition = launcherShot.ImpactPos;

        gameManager.BoardHit();
        deathTimer = 2.5f;
        GetComponent<Renderer>().enabled = false;
        Invoke("TilePainted", 0.5f);
    }

    public void TilePainted() {
        audioSource.PlayOneShot(tilePainted[Random.Range(0, tilePainted.Length)]);
    }

    void HitGround() {

        GameObject sparks = Instantiate(
            gameManager.sparksPrefabs[launcherShot.Owner.PlayerNumber - 1],
            launcherShot.ImpactPos,
            Quaternion.identity,
            gameManager.WorldAnchor);

        sparks.transform.localPosition = launcherShot.ImpactPos;
        deathTimer = 0.5f;

        //Vector3 shotDir = launcherShot.Velocity;
        //shotDir.y = 0;
        //shotDir.Normalize();
        //GameObject floorQuad = Instantiate(
        //floorQuadPrefab,
        //launcherShot.ImpactPos + (shotDir * floorHitScale * 0.4f),
        //Quaternion.LookRotation(shotDir),
        //gameManager.WorldAnchor);
        //Renderer quadRenderer = floorQuad.GetComponent<Renderer>();
        //quadRenderer.material.color = gameManager.GetPlayerColor(launcherShot.Owner.PlayerNumber);
        //quadRenderer.material.mainTextureScale = new Vector2(0.5f, 0.5f);
        //int xOffset = Random.Range(0, 2);
        //int yOffset = Random.Range(0, 2);

        //quadRenderer.material.mainTextureOffset = new Vector2(0.5f * xOffset, 0.5f * yOffset);
        //transform.localPosition = launcherShot.ImpactPos;
        //floorQuad.transform.localPosition = launcherShot.ImpactPos + (shotDir * floorHitScale * 0.4f);
        //floorQuad.transform.localScale = new Vector3(floorHitScale, floorHitScale, floorHitScale);
        Destroy(gameObject);
    }

    public bool SimulateTrajectory(
        Vector3 startPos, 
        Vector3 velocity, 
        out float timeToImpact, 
        out Vector3 impactPos, 
        bool drawLine
        ) {

        float timeStep = 0.1f;
        impactPos = Vector3.zero;
        float totalSteps = maxTime / timeStep;
        bool doesHit = false;
        List<Vector3> linePoints = new List<Vector3>();
        float _timeToImpact = maxTime;
        Vector3 _impactPos = Vector3.zero;

        for (int i = 0; i < totalSteps; i++) {
            Vector3 sliceStart = startPos + EvaluateTrajectory(i * timeStep, velocity);
            Vector3 sliceEnd = startPos + EvaluateTrajectory((i + 1) * timeStep, velocity);

            if (drawLine) linePoints.Add(sliceStart);

            Vector3 worldPosStart = gameManager.WorldAnchor.TransformPoint(sliceStart);
            Vector3 worldPosEnd = gameManager.WorldAnchor.TransformPoint(sliceEnd);
            if (CheckForCollision(worldPosStart, worldPosEnd, out _impactPos)) {
                _timeToImpact = i * timeStep;
                doesHit = true;
                break;
            }
        }

        if (drawLine) {
            GameObject lineObj = new GameObject("Line");
            lineObj.transform.SetParent(gameManager.WorldAnchor);
            lineObj.transform.localPosition = Vector3.zero;
            lineObj.transform.localRotation = Quaternion.identity;
            LineRenderer line = lineObj.AddComponent<LineRenderer>();
            line.material = firingLineMat;
            line.useWorldSpace = false;
            line.widthMultiplier = 0.01f;
            line.positionCount = linePoints.Count;
            line.SetPositions(linePoints.ToArray());
        }

        timeToImpact = _timeToImpact;
        impactPos = _impactPos;
        return doesHit;
    }

    public Vector3 EvaluateTrajectory(float time, Vector3 startVelocity) {
        Vector3 pos = Vector3.zero;
        pos = (startVelocity * time) + (gameManager.Gravity * time * time);
        return pos;
    }

    bool CheckForCollision(Vector3 start, Vector3 end, out Vector3 position) {
        LayerMask terrainMask = 1 << LayerMask.NameToLayer("Terrain");
        Vector3 direction = end - start;
        Debug.DrawLine(start, end, Color.red, 1000);
        float distance = direction.magnitude;
        Ray ray = new Ray(start, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, terrainMask)) {
            Debug.Log("_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_HIT " + hit.transform.name +"_+_+_+_+_+_+_+_+_+_+_+_+_+_");

            GameBoard board = hit.transform.GetComponentInParent<GameBoard>();
            launcherShot.BoardHit = board && board.Equals(gameManager.RemoteGameBoard);

            position = gameManager.WorldAnchor.InverseTransformPoint(hit.point);
            return true;
        }
        position = Vector3.down * 100;
        return false;
    }
}
