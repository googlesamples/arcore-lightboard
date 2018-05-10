//-----------------------------------------------------------------------
// <copyright file="UILaunchControl.cs" company="Google">
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
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UILaunchControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public UnityEvent launchEvent = new UnityEvent();
    public UnityEvent coolDownEvent = new UnityEvent();
    LauncherControl launcher;
    bool readyToFire = false;
    public bool ReadyToFire {
        get {
            return readyToFire;
        }
        set {
            readyToFire = value;
            trajectory.IsReady = value;
        }
    }

    float dragTimer = 0;
    float dragDuration = 0.15f;

    bool isDragging = false;
    bool IsDragging {
        get { return isDragging; }
        set {
            isDragging = value;
            dragTimer = Mathf.Clamp01(dragTimer);
        }
    }

    public bool IsCoolingDown {
        get { return !ReadyToFire && coolDown > 0; }
    }

    bool showCoolDownMeter = false;
    float coolDownMeterFade = 0.0f;

    public float CoolDown {
        get { return coolDown; }
    }

    Renderer handleRenderer;
    Transform connector;
    Renderer connectorRenderer;
    Transform lastShotHandle;
    Transform lastShotConnector;
    Renderer lastShotConnectorRenderer;
    Transform coolDownMeter;
    Renderer coolDownRenderer;

    AnimationCurve returnCurve;
    float returnTimer = 0.0f;
    float returnDuration = 0.1f;
    Vector3 returnStartPos;
    Vector3 returnEndPos;

    TrajectoryIndicator trajectory;
    public TrajectoryIndicator Trajectory {
        set { trajectory = value; }
    }

    bool isSetup = false;

    float minPullDistance = 1.2f;
    float maxPullDistance = 5.0f;

    float coolDownDuration = 3.0f;
    float coolDown = 0.0f;

    Color clearColor = new Color(1, 1, 1, 0);

    public UILaunchControl Init(LauncherControl newLauncher) {
        handleRenderer = transform.GetComponent<Renderer>();
        connector = transform.parent.Find("LauncherBar");
        connectorRenderer = connector.GetComponent<Renderer>();
        launcher = newLauncher;
        returnCurve = launcher.handleReturnCurve;
        isSetup = true;


        Color lastShotColor = launcher.PlayerColor;
        lastShotColor.a = 0.75f;

        lastShotHandle = transform.parent.Find("LastShotHandle");
        lastShotHandle.GetComponent<Renderer>().material.color = lastShotColor;
        lastShotHandle.gameObject.SetActive(false);

        lastShotConnector = transform.parent.Find("LastShotConnector");
        lastShotConnectorRenderer = lastShotConnector.GetComponent<Renderer>();
        lastShotConnectorRenderer.material.color = lastShotColor;
        lastShotConnector.gameObject.SetActive(false);



        coolDownMeter = transform.parent.Find("CoolDownMeter");
        coolDownRenderer = coolDownMeter.GetComponent<Renderer>();
        coolDownRenderer.material.SetColor("_Color", launcher.PlayerColor);

        SphereCollider handleColl = gameObject.AddComponent<SphereCollider>();
        handleColl.radius = 0.5f;

        return this;
    }

    void Update () {
        if (!isSetup)
            return;

        if (dragTimer >= 0.0f  && dragTimer <= 1.0f) {
            dragTimer += Time.deltaTime / (isDragging ? dragDuration : -dragDuration);
            float amount = Mathf.SmoothStep(0, 1, dragTimer);
            Color connectorColor = Color.Lerp(clearColor, Color.white, amount);
            Color handleColor = Color.Lerp(clearColor, Color.white, (amount * 0.5f) + 0.5f);
            connectorRenderer.material.color = connectorColor;
            handleRenderer.material.color = handleColor;
        }

        if (showCoolDownMeter && coolDownMeterFade <= 1.0f) {
            coolDownMeterFade += Time.deltaTime / dragDuration;
            float amount = Mathf.SmoothStep(0, 1, coolDownMeterFade);
            coolDownRenderer.material.SetFloat("_Fader", amount);
        }

        if (!showCoolDownMeter && coolDownMeterFade >= 0.0f) {
            coolDownMeterFade -= Time.deltaTime / dragDuration;
            float amount = Mathf.SmoothStep(0, 1, coolDownMeterFade);
            coolDownRenderer.material.SetFloat("_Fader", amount);
        }

        if (!ReadyToFire && coolDown > 0) {
            coolDown -= Time.deltaTime / coolDownDuration;
            coolDownRenderer.material.SetFloat("_Progress", coolDown);
            if (coolDown < 0) {
                showCoolDownMeter = false;
                coolDownEvent.Invoke();
            }
        }

        if (returnTimer < 1.0f) {
            returnTimer += Time.deltaTime / returnDuration;
            float amount = returnCurve.Evaluate(returnTimer);
            transform.localPosition = Vector3.Lerp(returnStartPos, returnEndPos, amount);
            connector.localScale = new Vector3(1, 1, transform.localPosition.magnitude);

            if (returnTimer > 1 && ReadyToFire) {
                ReadyToFire = false;
                launchEvent.Invoke();
            }
        }

        if (isDragging) {
            OnDrag();
        }

    }

    public void OnPointerDown(PointerEventData eventData) {
        if (!GameManager.Instance.LocalID.Equals(launcher.PlayerName))
            return;

        if (launcher.IsWaiting)
            return;

        if (IsCoolingDown) {
            launcher.PlayErrorSound();
            return;
        }
        IsDragging = true;
        launcher.PlayGrabSound();
    }

    public void OnDrag() {

        if (!IsDragging)
            return;

        Vector3 touchPos = GetTouchOnPlane(Input.mousePosition);
        Vector3 relPos = transform.parent.InverseTransformPoint(touchPos);
        float distance = Mathf.Min(relPos.magnitude, maxPullDistance);

        ReadyToFire = showCoolDownMeter = distance > minPullDistance;
        
        coolDown = 1.0f;
        Vector3 offset = (connector.position - transform.position).normalized;
        transform.position = touchPos;
        transform.localPosition = Vector3.ClampMagnitude(transform.localPosition, maxPullDistance);
        connector.rotation = Quaternion.LookRotation(offset, transform.parent.up);
        launcher.Heading = connector.localRotation.eulerAngles.y;
        launcher.Power = Mathf.InverseLerp(minPullDistance, maxPullDistance, distance);

        connector.localScale = new Vector3(1,1, Mathf.Min(distance, maxPullDistance));
        connectorRenderer.material.mainTextureScale = new Vector2(0.5f, Mathf.Min(distance, maxPullDistance));
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (launcher.IsWaiting)
            return;

        if (!IsDragging)
            return;

        IsDragging = false;
        returnStartPos = transform.localPosition;
        returnEndPos = transform.localPosition.normalized;
        returnEndPos.y = returnStartPos.y;
        returnTimer = 0.0f;

        if (launcher.IsPlayer) {
            lastShotHandle.gameObject.SetActive(true);
            lastShotHandle.localPosition = transform.localPosition;
            lastShotHandle.localRotation = transform.localRotation;
            lastShotConnector.gameObject.SetActive(true);
            lastShotConnector.localPosition = connector.localPosition;
            lastShotConnector.localRotation = connector.localRotation;
            lastShotConnector.localScale = connector.localScale;
            lastShotConnectorRenderer.material.mainTextureScale = connectorRenderer.material.mainTextureScale;

        }

        if (!ReadyToFire)
            coolDown = 0;
    }

    Vector3 GetTouchOnPlane(Vector3 screenPos) {
        Ray screenRay = Camera.main.ScreenPointToRay(screenPos);
        Plane floor = new Plane(transform.parent.up, connector.position);
        float distance = 0;
        Vector3 touchPos = Vector3.zero;
        if (floor.Raycast(screenRay, out distance)) {
            touchPos = screenRay.GetPoint(distance);
        }
        return touchPos;
    }
}
