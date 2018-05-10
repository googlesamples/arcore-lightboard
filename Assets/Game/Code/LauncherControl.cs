//-----------------------------------------------------------------------
// <copyright file="LauncherControl.cs" company="Google">
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
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LauncherControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    
    GameManager gameManager;
    GameBoard board;
    Island island;

    public string PlayerName {
        get { return island.PlayerName; }
    }

    bool isWaiting = true;
    public bool IsWaiting {
        get {
            return isWaiting;
        }
        set { 
            isWaiting = value;
        }
    }

    float introTimer = 0.0f;
    float introDuration = 0.5f;
    public AnimationCurve introCurve;


    public AnimationCurve tiltBackCurve;
    public AnimationCurve spinForwardCurve;
    public AnimationCurve handleReturnCurve;

    Transform launcherPivot;
    Transform launcherBase;
    Transform powerMeter;
    Renderer powerMeterRenderer;
    Transform launchPoint;
    UILaunchControl launchControl;
    AudioSource audioSource;
    public AudioClip[] launchSounds;
    public AudioClip endIntroSound;
    public AudioClip grabSound;
    public AudioClip errorSound;
    public AudioClip powerTickSound;


    bool shouldUpdate = true;
    bool settingsNeedUpdating = false;

    MultiplayerContol multiplayer;

    public Color PlayerColor {
        get { return island.PlayerColor; }
    }

    public int PlayerNumber {
        get { return island.PlayerNumber; }
    }

    float pitch = 0;
    public float Pitch {
        get { return pitch; }
        set { 
            pitch = Mathf.Clamp(value, -90 , 0);
            settingsNeedUpdating = true;
        }
    }

    float heading = 0;
    Quaternion headingRot = Quaternion.identity;
    public float Heading {
        get { return heading; }

        set { 
            heading = value; 
            headingRot = Quaternion.AngleAxis(heading, Vector3.up);
            settingsNeedUpdating = true;
        }
    }

    float power = 0.0f;
    Quaternion powerRot = Quaternion.identity;
    float powerAdjustment = 1.0f;
    float lastPower = 0.0f;
    public float Power {
        get {
            return power;
        }
        set {
            powerMeterRenderer.enabled = power > 0.01f;
            power = value;

            if (Mathf.FloorToInt(power * 5) != Mathf.FloorToInt(lastPower * 5)) {
                audioSource.PlayOneShot(powerTickSound, 0.3f);
            }
            powerRot = Quaternion.AngleAxis(power * 180, Vector3.right);
            settingsNeedUpdating = true;
            lastPower = power;
        }
    }
    public float TotalPower {
        get {
            return power + powerAdjustment;
        }
    }

    public bool IsPlayer {
        get { return gameObject.tag.Equals("Player"); }
    }

    bool hasFired = false;

    public void Init(string playerName, Island newIsland) {
        island = newIsland;
        gameObject.name = "Launcher_" + playerName;
        board = island.GameBoard;
        multiplayer = MultiplayerContol.Instance;
        gameManager = GameManager.Instance;
        launcherPivot = transform.Find("LauncherPivot");
        launcherBase = transform.Find("LauncherPivot/LauncherBase");
        powerMeter = transform.Find("LauncherPivot/LauncherBase/Meter");
        powerMeterRenderer = powerMeter.GetComponent<Renderer>();
        powerMeterRenderer.material.color = PlayerColor;
        powerMeterRenderer.material.SetColor("_EmissionColor", PlayerColor);
        launchPoint = transform.Find("LauncherPivot/LaunchPoint");

        launchControl = 
            transform.Find("LauncherHandle").gameObject.AddComponent<UILaunchControl>().Init(this);

        launchControl.launchEvent.AddListener(PlayerLaunch);
        launchControl.coolDownEvent.AddListener(CoolDownComplete);

        audioSource = transform.Find("AudioSource").GetComponent<AudioSource>();

        GameObject trajectoryObj = Instantiate(gameManager.trajectoryPrefab, gameManager.WorldAnchor);
        TrajectoryIndicator trajectory = trajectoryObj.GetComponent<TrajectoryIndicator>();
        trajectory.LaunchPoint = launchPoint;
        trajectory.LauncherControl = this;
        launchControl.Trajectory = trajectory;

        Pitch = -45;

        if (gameObject.tag.Equals("Player")) { 
            StartCoroutine(PostSettings());
        } else {
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
			
  	}

	void Update () {
        if (isWaiting) return;

        if (introTimer < 1.0f) {
            introTimer += Time.deltaTime / introDuration;
            float amount = introCurve.Evaluate(introTimer);
            transform.position = Vector3.Lerp(
                board.StartPose.position,
                board.ReadyPose.position,
                amount);
            transform.rotation = Quaternion.Lerp(
                board.StartPose.rotation,
                board.ReadyPose.rotation,
                amount);
            if (introTimer > 1.0f) {
                audioSource.PlayOneShot(endIntroSound);
            }
            return;
        } 


        launcherPivot.localRotation = Quaternion.RotateTowards(
            launcherPivot.localRotation, 
            headingRot, 
            Time.deltaTime * 360);


        if (hasFired) {
            launcherBase.localRotation = Quaternion.AngleAxis(
                (spinForwardCurve.Evaluate(1 - launchControl.CoolDown) * -1480),
                Vector3.right);
        } else {
            launcherBase.localRotation = Quaternion.AngleAxis(power * -60, Vector3.right);
        }

        powerMeter.localRotation = powerRot;
	}

    public void OnPointerDown(PointerEventData eventData) {
        launchControl.OnPointerDown(eventData);
    }
    public void PlayGrabSound() {
        audioSource.PlayOneShot(grabSound);
    }

    public void PlayErrorSound() {
        audioSource.PlayOneShot(errorSound);
    }

    public void OnPointerUp(PointerEventData eventData) {
        launchControl.OnPointerUp(eventData);
    }

    IEnumerator PostSettings() {
        while (shouldUpdate) {
            yield return new WaitForSeconds(1.0f);

            if (settingsNeedUpdating && IsPlayer) {
                Vector3 settings = new Vector3(
                    Heading, Pitch, Power
                );
                multiplayer.SetPlayerSettings(island.PlayerName, settings);
                settingsNeedUpdating = false;
                Debug.Log("Posting Settings");
            }
        }
    }

    public void GetNetworkUpdate(object sender2, ValueChangedEventArgs data) {
        if (data.Snapshot != null && data.Snapshot.Exists) {
            Heading = float.Parse(data.Snapshot.Child("heading").Value.ToString());
            Pitch = float.Parse(data.Snapshot.Child("pitch").Value.ToString());
            Power = float.Parse(data.Snapshot.Child("power").Value.ToString());
        }
    }

    GameObject GetProjectile(float shotPower) {
        GameObject prefrabGroup = GameManager.Instance.GetProjectile();
        int size = Mathf.CeilToInt(shotPower * 4);
        Debug.Log("Getting slice " + size);

        return prefrabGroup.transform.Find("Slice" + size).gameObject;
    }

    public void ForceLaunch(LauncherShot launcherShot) {
        Debug.Log(PlayerName + " is firing becuase of the networks!");

        GameObject projectileObj = Instantiate(
            GetProjectile(launcherShot.PowerSetting),
            launchPoint.position,
            launchPoint.rotation,
            GameManager.Instance.WorldAnchor
        );

        Renderer projRenderer = projectileObj.GetComponent<Renderer>();
        projRenderer.material.color = PlayerColor;
        projRenderer.material.SetColor("_EmissionColor", PlayerColor);

        Projectile projectile = projectileObj.GetComponent<Projectile>();
        launcherShot.IsReplay = true;
        audioSource.PlayOneShot(launchSounds[Random.Range(0, launchSounds.Length)]);
        projectile.Launch(launcherShot);
        hasFired = true;
    }

    public void PlayerLaunch() {
        StartCoroutine(CompletePlayerLaunch());
        hasFired = true;
    }

    IEnumerator CompletePlayerLaunch() {

        yield return new WaitForSeconds(0.1f);

        Debug.Log("FIRE!");
        GameObject projectileObj = Instantiate(
            GetProjectile(power),
            launchPoint.position,
            launchPoint.rotation,
            GameManager.Instance.WorldAnchor
        );

        Renderer projRenderer = projectileObj.GetComponent<Renderer>();
        projRenderer.material.color = PlayerColor;
        projRenderer.material.SetColor("_EmissionColor", PlayerColor);

        Projectile projectile = projectileObj.GetComponent<Projectile>();

        Vector3 launchVel = 
            GameManager.Instance.WorldAnchor.InverseTransformDirection(launchPoint.forward * TotalPower);

        LauncherShot launcherShot = new LauncherShot("shot", this, projectileObj.transform.localPosition, launchVel);

        launcherShot.PowerSetting = power;
        Power = 0.0f;
        audioSource.PlayOneShot(launchSounds[Random.Range(0, launchSounds.Length)]);
        projectile.Launch(launcherShot);

        if (gameObject.tag.Equals("Player")) {
            GameManager.Instance.PlayerShot(launcherShot);
        }       
    }

    public void CoolDownComplete() {
        hasFired = false;
    }
}
