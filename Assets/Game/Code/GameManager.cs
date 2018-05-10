//-----------------------------------------------------------------------
// <copyright file="GameManager.cs" company="Google">
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
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
    static GameManager instance;
    ARCoreController arCoreController;
    public ARCoreController ARCoreController {
        get { return arCoreController; }
    }
    ARKitController arKitController;
    public ARKitController ARKitController {
        get { return arKitController; }
    }

    public static GameManager Instance {
        get {
            return instance;
        }
    }

    private void CreateInstance() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public enum GameState { Lobby, Joining, Placing, Playing, Over }
    GameState gameState = GameState.Lobby;
    public GameState CurrentGameState {
        get { return gameState; }
        set { gameState = value; }
    }

    bool gameOver = false;


    MultiplayerContol multiplayer;

    public Vector3 Gravity {
        get { return new Vector3(0, -1, 0); }
    }

    public Vector3 GridSize {
        get { return new Vector3(0.7f, 0.5f, 0.7f); }
    }

    public Vector3 GridDivisions {
        get { return new Vector3(7, 1, 7); }
    }

    float introTimer = -1.0f;

    string localID = "Empty";
    public string LocalID {
        get { return localID; }
    }

    string remoteID = "Empty";
    public string RemoteID {
        get { return remoteID; }
    }   

    LauncherControl localLauncher;
    LauncherControl remoteLauncher;

    GameBoard localGameBoard;
    public GameBoard LocalGameBoard {
        get { return localGameBoard; }
    }

    GameBoard remoteGameBoard;
    public GameBoard RemoteGameBoard {
        get { return remoteGameBoard; }
    }

    public LauncherControl GetLauncher(string playerName) {
        if (localID.Equals(playerName))
            return localLauncher;
        
        if (remoteID.Equals(playerName))
            return remoteLauncher;

        return null;
    }

    UIControl uiControl;
    Transform worldAnchor;
    public Transform WorldAnchor {
        get { return worldAnchor; }
    }

    public GameObject boardPrefab;
    public GameObject placerPrefab;
    public GameObject islandPrefab;
    public GameObject trajectoryPrefab;
    public GameObject launcherPrefab;
    public GameObject projectilePrefab;
    public GameObject projectileGlowPrefab;
    public GameObject tileDebisPrefab;
    public GameObject[] sparksPrefabs;


    public Color player0Color;
    public Color player1Color;
    public Color player2Color;

    public Material player0InnerMaterial;
    public Material player0OuterMaterial;
    public Material player1InnerMaterial;
    public Material player1OuterMaterial;
    public Material player2InnerMaterial;
    public Material player2OuterMaterial;


    void Awake() {
        CreateInstance();

        string playerID = PlayerPrefs.GetString("playerID");
        if (playerID == "") {
            playerID = Mathf.FloorToInt(Random.value * 10000).ToString("D5");
            PlayerPrefs.SetString("playerID", playerID);
        }
        Debug.Log("PlayerID is " + playerID);
        localID = playerID;


        uiControl = GameObject.Find("GameUI").GetComponent<UIControl>();
            }

	private void Start() {
        multiplayer = MultiplayerContol.Instance;
        GameObject arControllerObj = GameObject.Find("ARController");
        if (arControllerObj) {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                arKitController = arControllerObj.GetComponent<ARKitController>();
            } else {
                arCoreController = arControllerObj.GetComponent<ARCoreController>();
            }
        }
        PhysicsRaycaster caster = Camera.main.gameObject.GetComponent<PhysicsRaycaster>();
        if (!caster) {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
	}

	private void Update() {
        if (introTimer > 0) {
            introTimer -= Time.deltaTime;
            if (introTimer < 0) {
                localLauncher.IsWaiting = false;
                remoteLauncher.IsWaiting = false;
            }
        }
	}

	public void HostGame(int roomNumber) {
        multiplayer.IsHost = true;
        multiplayer.RoomNumber = roomNumber;

        uiControl.ShowTips(true);
        uiControl.ShowProgress(true);
        uiControl.ShowLobby(false);
        CurrentGameState = GameState.Joining;

        if (Application.platform == RuntimePlatform.OSXEditor ||
            Application.platform == RuntimePlatform.WindowsEditor) {

            GameObject testAnchor = new GameObject("test anchor");
            testAnchor.transform.position = transform.position;
            testAnchor.transform.rotation = transform.rotation;

            worldAnchor = testAnchor.transform;
            multiplayer.SetRoomStatus(roomNumber, "waiting");
            Invoke("EnterGame", 5);
        } else {
            multiplayer.SetRoomStatus(roomNumber, "locked");
            Debug.Log("Hosting Game on Device");
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                arKitController.HostAnchor(HostingResults);
            } else {
                arCoreController.HostAnchor(HostingResults);
            }
        }
    }

    void HostingResults(Transform anchor, string anchorID) {
        worldAnchor = anchor;
        multiplayer.SetRoomCloudId(anchorID);
        multiplayer.SetRoomStatus(multiplayer.RoomNumber, "waiting");
        EnterGame();
    }

    public void JoinEditorGame(int roomNumber) {
        GameObject testAnchor = new GameObject("test anchor");
        testAnchor.transform.position = transform.position;
        testAnchor.transform.rotation = transform.rotation;
        worldAnchor = testAnchor.transform;
        multiplayer.IsHost = false;
        multiplayer.RoomNumber = roomNumber;
        Invoke("EnterGame", 5);
        CurrentGameState = GameState.Joining;

    }

    public void JoinGame(int roomNumber, Transform anchor) {
        worldAnchor = anchor;
        multiplayer.IsHost = false;
        multiplayer.RoomNumber = roomNumber;
        EnterGame();
        CurrentGameState = GameState.Joining;

    }

    public void AbortGame(string message) {
        uiControl.OpenMessageBox(message);

        Debug.Log("Aborting becuase: " + message);

        if (WorldAnchor) {
            foreach (Transform item in WorldAnchor) {
                if (!item.name.Equals("FloorPlane"))
                    Destroy(item.gameObject);
            }
        }

        uiControl.ShowTips(false);
        uiControl.ShowProgress(false);
        uiControl.ShowLobby(true);
        multiplayer.SetRoomStatus(multiplayer.RoomNumber, "done");
        CurrentGameState = GameState.Lobby;

    }

    public void EnterGame() {
        Debug.Log(localID + " is entering the game");

        uiControl.ShowTips(false);
        uiControl.ShowProgress(false);

        multiplayer.SetRestart(false);
        multiplayer.AddPlayer(localID);
        multiplayer.AddPlayersListener();
        multiplayer.AddRemoveListener();
        multiplayer.AddShotListener();
        multiplayer.AddRestartListener();
        multiplayer.ClearShotsFromRoom();
        multiplayer.ClearDebrisFromRoom();
        AddPlacer(true, localID);
        CurrentGameState = GameState.Placing;
    }

    public void ReEnterGame() {
        foreach(Transform item in WorldAnchor) {
            if (!item.name.Equals("FloorPlane"))
                Destroy(item.gameObject);
        }
        multiplayer.ClearShotsFromRoom();
        multiplayer.ClearDebrisFromRoom();
        uiControl.HideWinScreen();

        gameOver = false;
        CurrentGameState = GameState.Placing;

        StartCoroutine(FinishRestart());
    }

    IEnumerator FinishRestart() {
        yield return new WaitForSeconds(1.0f);
        AddPlacer(true, localID);
        AddPlacer(false, remoteID);  
    }


    public void AddPlacer(bool isPlayer, string playerName) {
        GameObject newPlacer = Instantiate(placerPrefab) as GameObject;
        IslandPlacer islandPlacer = newPlacer.GetComponent<IslandPlacer>();
        islandPlacer.transform.SetParent(worldAnchor);

        islandPlacer.transform.localRotation = Quaternion.identity;

        islandPlacer.Init(playerName, isPlayer);

        if (!isPlayer)
            multiplayer.AddIslandListener(playerName, islandPlacer.GetNetworkUpdate);
    }

    public void PlaceIsland(Vector3 pos, Quaternion rot) {
        PlaceIsland(true, localID, pos, rot);
    }

    public void PlaceIsland(string playerName, Vector3 pos, Quaternion rot) {
        PlaceIsland(false, playerName, pos, rot);
    }

    public void PlaceIsland(bool isPlayer, string playerName, Vector3 pos, Quaternion rot) {
        Debug.Log("Placing Island for " + playerName);
        GameObject newIsland = Instantiate(islandPrefab) as GameObject;
        Island island = newIsland.GetComponent<Island>();

        if (isPlayer) {
            island.PlayerNumber = multiplayer.IsHost ? 2 : 1;
        } else {
            island.PlayerNumber = multiplayer.IsHost ? 1 : 2;
        }

        island.Init(isPlayer, playerName);

        if (isPlayer) {
            localGameBoard = island.GameBoard;
            island.worldReady.AddListener(WorldReady);
        } else {
            remoteGameBoard = island.GameBoard;
        }

        island.transform.SetParent(worldAnchor);
        island.transform.localPosition = pos;
        island.transform.localRotation = rot;

        multiplayer.SetRestart(false);
        UpdateIslandSettings(playerName, pos, rot, true, true);
        CurrentGameState = GameState.Playing;
    }

    public void UpdateIslandSettings(string playerName, Vector3 pos, Quaternion rot, bool isReady, bool isPlaced) {
        multiplayer.UpdateIsland(playerName, pos, rot, isReady, isPlaced);
    }

    public void WorldReady() {
        introTimer = 5.0f;
        uiControl.OpenCountDown();
    }

    public Color GetPlayerColor(int playerNumber) {
        Color playerColor = Color.white;
        switch (playerNumber) {
            case 0:
                playerColor = player0Color;
                break;
            case 1:
			    playerColor = player1Color;
                break;
            case 2:
			    playerColor = player2Color;
                break;
            default:
                Debug.Log("_++_+_+_+_ PLayer number is wrong");
                break;
        }
        return playerColor;
    }

    public Material[] GetPlayerMaterials(int playerNumber) {
        Material[] playerMat = new Material[2];
        switch (playerNumber) {
            case 0:
                playerMat[0] = player0InnerMaterial;
                playerMat[1] = player0OuterMaterial;
                break;
            case 1:
                playerMat[0] = player1InnerMaterial;
                playerMat[1] = player1OuterMaterial;
                break;
            case 2:
                playerMat[0] = player2InnerMaterial;
                playerMat[1] = player2OuterMaterial;
                break;
            default:
                Debug.Log("_++_+_+_+_ PLayer number is wrong");
                break;
        }
        return playerMat;
    }

    public void AddLauncher(LauncherControl newLauncher) {

        multiplayer.AddPlayerLauncher(localID, newLauncher.transform.localPosition, newLauncher.transform.localEulerAngles);

        if (newLauncher.gameObject.tag.Equals("Player")) {
            localLauncher = newLauncher;
        } else {
            Debug.Log("Adding setting listner for " + newLauncher.name);
            multiplayer.AddLauncherListener(newLauncher.PlayerName, newLauncher.GetNetworkUpdate);
            remoteLauncher = newLauncher;
            remoteID = newLauncher.PlayerName;
        }
    }

    public GameObject GetProjectile() {
        return projectilePrefab;
    }

    public void PlayerShot(LauncherShot shot) {
        multiplayer.AddShot(shot);
    }

    public void EnemyShot(LauncherShot shot) {
        Debug.Log(shot.OwnerName + " took shot " + shot.Name);
        remoteLauncher.ForceLaunch(shot);
    }

    public void RemoveLauncher(string playerName) {
        Debug.Log("Should remove " + playerName);

    }

    private void OnApplicationQuit() {
        if (!string.IsNullOrEmpty(localID))
            multiplayer.DisconnectPlayer(localID);
    }

    private void OnApplicationFocus(bool isFocused) {
        if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.OSXEditor)
            return;
        
        if (isFocused) {
            if (!string.IsNullOrEmpty(localID))
                multiplayer.ReconnectPlayer(localID);    
        } else {
            if (!string.IsNullOrEmpty(localID))
                multiplayer.DisconnectPlayer(localID);
        }
    }

    public void ShowMessage(string message, float time) {
        uiControl.ShowHelpText(message, time);
    }

    public void BoardHit() {
        localGameBoard.CheckScore();
        remoteGameBoard.CheckScore();
    }

    public void AddDebris(Vector3 pos, Vector3 offset, Renderer tile, string debrisName, int playerNumber) {
        GameObject debrisObj = Instantiate(
            tileDebisPrefab, worldAnchor.TransformPoint(pos), Quaternion.identity, WorldAnchor);
        debrisObj.GetComponent<TileDebris>().Init(pos, tile, offset, debrisName, playerNumber);
        multiplayer.AddDebris(debrisName);
    }

    public void SetScore(string player, int painted, int total) {
        if (gameOver) return;
        gameOver = painted >= total;
        CurrentGameState = GameState.Over;
        uiControl.SetScore(player, painted, total);
    }

    public void PlayClip(AudioClip clip) {
        uiControl.AudioSource.PlayOneShot(clip);
    }

  
}