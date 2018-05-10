//-----------------------------------------------------------------------
// <copyright file="BuildProject.cs" company="Google">
//
// Copyright 2016 Google LLC. All Rights Reserved.
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


using UnityEditor;
using UnityEngine;

public class BuildProject : MonoBehaviour {
    [MenuItem("Build/Build Android")]
    public static void BuildAndroid() {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Game/Scenes/ARCoreScene.unity" };
        buildPlayerOptions.locationPathName = "Builds/Android";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
    [MenuItem("Build/Build iOS")]
    public static void MyBuild() {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Game/Scenes/ARKitScene.unity" };
        buildPlayerOptions.locationPathName = "Builds/iOS";
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}
