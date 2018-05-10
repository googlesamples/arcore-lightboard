//-----------------------------------------------------------------------
// <copyright file="ARKitController.cs" company="Google">
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
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using UnityEngine.XR.iOS;
using GoogleARCore.Examples.CloudAnchor;

public class ARKitController : MonoBehaviour {

    private ARKitHelper arKitHelper = new ARKitHelper();
    private MultiplayerContol multiplayer;
    public Transform floorPlane;
    public GameObject snackBarUI;

    private bool trackingComplete = false;
    private Text debugTextField;

    public GameObject planeVisualizer;
    private UnityARAnchorManager unityARAnchorManager;

    bool hostingInProgress = false;

    private void Start() {
        unityARAnchorManager = new UnityARAnchorManager();
        UnityARUtility.InitializePlanePrefab(planeVisualizer);

        debugTextField = GameObject.Find("GameUI/DebugText").GetComponent<Text>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    //    multiplayer = MultiplayerContol.Instance;
        floorPlane.GetComponent<Renderer>().enabled = false;
    }

    private void Update() {
        if (trackingComplete)
            return;
    }

    public delegate void HostAnchorResults(Transform worldAnchor, string AnchorId);
    public void HostAnchor(HostAnchorResults hostingResults) {
        Debug.Log("Hosting Anchor");

        hostingInProgress = true;

        Component worldAnchor;
        if (CreateAnchor(out worldAnchor)) {

            Debug.Log("Point found, creating anchor");

#if !UNITY_IOS
            var hostAnchor = (Anchor)worldAnchor;
#else
            var hostAnchor = (UnityARUserAnchorComponent)worldAnchor;
#endif
            SetFloorPlane(hostAnchor.transform);
                Debug.Log("Hosting Anchor in ARKit");

            XPSession.CreateCloudAnchor(hostAnchor).ThenAction(result => {
                
                if (!hostingInProgress) return;

                if (result.Response != CloudServiceResponse.Success) {
                    var errorString = string.Format("Failed to HOST cloud anchor: {0}.", result.Response);
                    Abort(errorString);
                    return;
                }

                Debug.Log("Hosting Complete with anchor: " + result.Anchor.CloudId);
                Debug.Log("Anchor tracking state: " + result.Anchor.TrackingState.ToString());

                Transform anchorTransform = result.Anchor.transform;
                hostingResults(anchorTransform, result.Anchor.CloudId);
                SetFloorPlane(anchorTransform);
                hostingInProgress = false;
                TrackingComplete();
            });

        } else {
            Debug.Log("Failed to host anchor");
            Abort("Failed to host anchor");
        }
    }

    public delegate void ResolveAnchorResults(Transform worldAnchor);
    public void ResolveAnchor(string cloudID, ResolveAnchorResults resolveResults) {

        XPSession.ResolveCloudAnchor(cloudID).ThenAction(result => {
            if (result.Response != CloudServiceResponse.Success) {
                var errorString = string.Format("Failed to RESOLVE cloud anchor: {0}.", result.Response);
                Debug.LogError(errorString);
                Abort("Could not find your anchor... Try holding the devices close together. \n" + errorString);
                return;
            }

            Debug.LogFormat("Resolved at {0}", result.Anchor.transform.position);
            SetFloorPlane(result.Anchor.transform);
            resolveResults(result.Anchor.transform);
            TrackingComplete();
        });
    }

    bool CreateAnchor (out Component anchor) {
        Vector2 screenCenter = new Vector2 (Screen.width / 2.0f, Screen.height / 2.0f);

        Pose hitPose;
        if (arKitHelper.RaycastPlane(Camera.main, screenCenter.x, screenCenter.y, out hitPose)) {
            anchor = arKitHelper.CreateAnchor(hitPose);
            return true;
        } else {
            Abort("Didn't hit a plane... Point your device at plane and try again");
        }

        anchor = null;
        return false;
    }

    void TrackingComplete() {

        Debug.Log("Tracking Complete");

        UnityARSessionNativeInterface m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();
        ARKitWorldTrackingSessionConfiguration config = new ARKitWorldTrackingSessionConfiguration();
        config.planeDetection = UnityARPlaneDetection.None;
        config.alignment = UnityARAlignment.UnityARAlignmentGravity;
        config.getPointCloudData = false;
        config.enableLightEstimation = false;
        m_session.RunWithConfig(config);

        IEnumerable<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors();
        foreach (var planeAnchor in arpags) {
           planeAnchor.gameObject.SetActive(false);
        }
    }

    void Abort(string message) {
        Debug.Log("Something failed");
        hostingInProgress = false;
        trackingComplete = false;

        IEnumerable<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors();
        foreach (var planeAnchor in arpags) {
            planeAnchor.gameObject.SetActive(true);
        }
        GameManager.Instance.AbortGame(message);

        UnityARSessionNativeInterface m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();
        ARKitWorldTrackingSessionConfiguration config = new ARKitWorldTrackingSessionConfiguration();
        config.planeDetection = UnityARPlaneDetection.Horizontal;
        config.alignment = UnityARAlignment.UnityARAlignmentGravity;
        m_session.RunWithConfig(config);
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

    void OnDestroy() {
        unityARAnchorManager.Destroy();
    }

}


