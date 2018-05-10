//-----------------------------------------------------------------------
// <copyright file="ARCoreController.cs" company="Google">
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


using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using System.Collections.Generic;

public class ARCoreController : MonoBehaviour {

    public Transform floorPlane;
    public GameObject snackBarUI;
    public GameObject trackedPlanePrefab;

    private bool trackingComplete = false;
    private Text debugTextField;
    private bool isQuitting;

    private List<DetectedPlane> newPlanes = new List<DetectedPlane>();
    private List<DetectedPlane> allPlanes = new List<DetectedPlane>();
    private List<GameObject> visualizers = new List<GameObject>();

    private void Start() {
        debugTextField = GameObject.Find("GameUI/DebugText").GetComponent<Text>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        floorPlane.GetComponent<Renderer>().enabled = false;
    }


    public void Update() {

        _QuitOnConnectionErrors();

        if (trackingComplete) 
            return;

        if (Session.Status != SessionStatus.Tracking) {
            const int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
            if (!isQuitting && Session.Status.IsValid()) {
                snackBarUI.SetActive(true);
            }

            return;
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Session.GetTrackables(newPlanes, TrackableQueryFilter.New);
        for (int i = 0; i < newPlanes.Count; i++) {
            GameObject planeObject = Instantiate(trackedPlanePrefab, Vector3.zero, Quaternion.identity,
                transform);
            planeObject.GetComponent<GoogleARCore.Examples.Common.DetectedPlaneVisualizer>().Initialize(newPlanes[i]);
            visualizers.Add(planeObject);
        }

        Session.GetTrackables<DetectedPlane>(allPlanes);
        bool showSearchingUI = true;
        for (int i = 0; i < allPlanes.Count; i++) {
            if (allPlanes[i].TrackingState == TrackingState.Tracking) {
                showSearchingUI = false;
                break;
            }
        }

        snackBarUI.SetActive(showSearchingUI);

    }

    bool FindPointOnPlane(out Pose pose) {
        bool didHit = false;
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        Vector2 screenPos = new Vector2(Screen.width/2, Screen.height/2) ;
        didHit = Frame.Raycast(screenPos.x, screenPos.y, raycastFilter, out hit);
        pose = hit.Pose;
        return didHit;
    }

    public delegate void HostAnchorResults(Transform worldAnchor, string AnchorId);
    public void HostAnchor(HostAnchorResults hostingResults) {
        Debug.Log("Hosting Anchor");

        if (allPlanes.Count < 1) {
            Abort("No planes found yet... Try moving your device around more");
            return;
        }

        Pose anchorPose;
        bool didHit = FindPointOnPlane(out anchorPose);
        if (!didHit) {
            Abort("Didn't hit a plane... Point your device at plane and try again");
            return;
        }
        Debug.Log("Point found, creating anchor");

        Anchor hostAnchor = Session.CreateAnchor(anchorPose);

        SetFloorPlane(hostAnchor.transform);

        XPSession.CreateCloudAnchor(hostAnchor).ThenAction(result => {
            if (result.Response != CloudServiceResponse.Success) {
                var errorString = string.Format("Failed to HOST cloud anchor: {0}.", result.Response);
                Abort(errorString);
                return;
            }
            Debug.Log("Responce is: " + result.Response.ToString());

            //if (result.Anchor.TrackingState != XPTrackingState.Tracking) {
            //    var errorString = string.Format("Anchor not tracking: {0}.", result.Anchor.TrackingState.ToString());
            //    Abort(errorString);
            //    return;
            //}

            Debug.Log("Hosting Complete with anchor: " + result.Anchor.CloudId);
            Debug.Log("Anchor tracking state: " + result.Anchor.TrackingState.ToString());

            Debug.Log(result.Anchor.transform.position.ToString());

            hostingResults(result.Anchor.transform, result.Anchor.CloudId);
            TrackingComplete();
        });

    }

    public delegate void ResolveAnchorResults(Transform worldAnchor);
    public void ResolveAnchor(string cloudID, ResolveAnchorResults callback) {

        XPSession.ResolveCloudAnchor(cloudID).ThenAction(result => {
            if (result.Response != CloudServiceResponse.Success) {
                var errorString = string.Format("Failed to RESOLVE cloud anchor: {0}.", result.Response);
                Debug.LogError(errorString);
                Abort("Could not find your anchor... Try holding the devices close together. " + errorString);
                return;
            }

            Debug.Log(string.Format("RESOLVED {0}", result.Anchor.CloudId));

            Debug.LogFormat("Resolved at {0}", result.Anchor.transform.position);
            SetFloorPlane(result.Anchor.transform);
            callback(result.Anchor.transform);
            TrackingComplete();
        });
    }

    public void SetDebugText(string message) {
        debugTextField.text = message;
        Debug.Log(message);
    }

    void SetFloorPlane(Transform anchor) {
        floorPlane.SetParent(anchor);
        floorPlane.localPosition = new Vector3(0.0f, -0.1f, 0.0f);
        floorPlane.localRotation = Quaternion.identity;
        floorPlane.GetComponent<Renderer>().enabled = true;

    }

    private void _QuitOnConnectionErrors() {
        if (isQuitting) {
            return;
        }

        if (Session.Status == SessionStatus.ErrorPermissionNotGranted) {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            isQuitting = true;
            Invoke("_DoQuit", 0.5f);
        } else if (Session.Status.IsError()) {
            _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
            isQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }

    private void _DoQuit() {
        Application.Quit();
    }

    private void _ShowAndroidToastMessage(string message) {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null) {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, 0);
                toastObject.Call("show");
            }));
        }
    }

    void TrackingComplete() {
        trackingComplete = true;
        foreach (GameObject visualizer in visualizers) {
            Destroy(visualizer);
        }
    }

    void Abort(string message) {
        Debug.Log("Aborting Game");
        trackingComplete = false;
        GameManager.Instance.AbortGame(message);
    }
}


