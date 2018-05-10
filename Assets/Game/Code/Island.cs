//-----------------------------------------------------------------------
// <copyright file="Island.cs" company="Google">
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Island : MonoBehaviour {

    public float CubeSize {
        get { return gameBoard.CubeSize.x; }
    }

    public Vector3 WorldSize {
        get { return gameBoard.GridSize; }
    }

    GameBoard gameBoard;
    public GameBoard GameBoard {
        get { return gameBoard; }
    }

    GameManager gameManager;

    bool isPlayerIsland;

    string playerName;
    public string PlayerName {
        get { return playerName; }
    }

    Color playerColor;
    public Color PlayerColor {
        get { return playerColor; }
        set { playerColor = value; }
    }

    int playerNumber = 0;
    public int PlayerNumber {
        get { return playerNumber; }
        set {
            playerNumber = value;
            PlayerColor = GameManager.Instance.GetPlayerColor(playerNumber);
        }
    }

    LauncherControl launcher;
    public LauncherControl Launcher {
        get { return launcher; }
    }
    
    public UnityEvent worldReady = new UnityEvent();

    public void Init(bool isPlayer, string newPlayerName) {
        isPlayerIsland = isPlayer;
        gameManager = GameManager.Instance;
        playerName = newPlayerName;
        gameObject.name = "Island_" + playerName;
        gameBoard = transform.Find("GameBoard").GetComponent<GameBoard>();
        gameBoard.terrainReady.AddListener(IslandComplete);
        gameBoard.Init();        
    }

    public void IslandComplete() {
        worldReady.Invoke();
        AddLauncher();
    }

    void AddLauncher() {
        GameObject launcherObj = Instantiate( gameManager.launcherPrefab, transform);
        launcherObj.tag = isPlayerIsland ? "Player" : "Enemy";
        launcherObj.transform.position = gameBoard.StartPose.position;
        launcherObj.transform.rotation = gameBoard.StartPose.rotation;
        launcherObj.transform.localScale = Vector3.one * CubeSize;
        launcher = launcherObj.GetComponent<LauncherControl>();
        launcher.Init(playerName, this);
        gameManager.AddLauncher(Launcher);

    }
}
