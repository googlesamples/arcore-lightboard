//-----------------------------------------------------------------------
// <copyright file="MultiplayerContol.cs" company="Google">
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

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

using System;
using UnityEngine.Events;

public class MultiplayerContol : MonoBehaviour {


    static MultiplayerContol instance;

    public static MultiplayerContol Instance {
        get { return instance; }
    }

    private void CreateInstance() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }


    GameManager gameManager;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

    int thisRoomNumber = -1;
    public int RoomNumber {
        get { return thisRoomNumber; }
        set { thisRoomNumber = value; }
    }

    bool isHost = true;
    public bool IsHost {
        get { return isHost; }
        set { isHost = value; }
    }

    void Awake() {
        CreateInstance();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                InitializeFirebase();
            } else {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
        StartCoroutine(FindGameManager());
    }

    IEnumerator FindGameManager() {
        yield return new WaitForSeconds(0.1f);
        gameManager = GameManager.Instance;
    }

    protected virtual void InitializeFirebase() {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://artillerydatabase.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
    }

    public void AddPlayersListener() {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players").ChildAdded += OnPlayerAdded;
    }

    public void AddRemoveListener() {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players").ChildRemoved += OnPlayerRemoved;
        
    }

    public void AddShotListener() {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/shots").ChildAdded += OnShotAdded;
    }

    public void RemoveShotListener() {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/shots").ChildAdded -= OnShotAdded;
    }

    public void AddRestartListener() {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/restart").ValueChanged += OnRestartChanged;
    }

    public void AddLauncherListener(string playerName, EventHandler<ValueChangedEventArgs> callback) {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players/" + playerName + "/settings").ValueChanged += callback;
    }

    public void RemoveLauncherListener(string playerName, EventHandler<ValueChangedEventArgs> callback) {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players/" + playerName + "/settings").ValueChanged -= callback;
    }

    public void SetRestart(bool setting) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/restart");
        reference.SetValueAsync(setting ? "true" : "false");
        SetRoomStatus(thisRoomNumber, "done");
    }

    public void SetPlayerSettings(string playerName, Vector3 settings) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players/" + playerName + "/settings");
        Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
        newScoreMap["heading"] = (float)decimal.Round((decimal)settings.x, 3);
        newScoreMap["pitch"] = (float)decimal.Round((decimal)settings.y, 3);
        newScoreMap["power"] = (float)decimal.Round((decimal)settings.z, 3);
        reference.SetValueAsync(newScoreMap);
    }

    public delegate void PlayerCountEvent(string roomChecked, int count);
    public void GetPlayerCount(string roomToTest, PlayerCountEvent callback) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomToTest + "/players");

        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("No game data");
            } else if (task.IsCompleted) {
                DataSnapshot data = task.Result;
                if (data.Exists) {
                    callback(roomToTest, int.Parse(data.ChildrenCount.ToString()));
                } else {
                    callback(roomToTest, 0);
                }
            }
        });
    }

    public void OnPlayerAdded(object sender2, ChildChangedEventArgs data) {
        if (data.Snapshot == null)
            Debug.Log("What the... player is null");


        Debug.Log("Looking at " + data.Snapshot.Key);

        //Make sure this player isn't us!
        if (gameManager.LocalID.Equals(data.Snapshot.Key)) {
            Debug.Log("We dont need to add ourselves");

        } else if (gameManager.RemoteID.Equals("Empty")) {// if we don't know about this player add it!
            Debug.Log("Wow, " + data.Snapshot.Key + " is a new player!");

            gameManager.AddPlacer(false, data.Snapshot.Key);
        } else {
            Debug.Log("What, a room is already full! Somethign went wrong!");
        }
    }

    void OnPlayerRemoved(object sender2, ChildChangedEventArgs data) {
        if (data.Snapshot != null) {
            Debug.Log(data.Snapshot.Key + " Removed!");
            gameManager.RemoveLauncher(data.Snapshot.Key);
        }
        IsHost = true;
    }

    public void OnShotAdded(object sender2, ChildChangedEventArgs data) {
        Debug.Log("A new shot is added");

        if (data.Snapshot != null) {
            string shotName = data.Snapshot.Child("name").Value.ToString();
            string shotOwner = data.Snapshot.Child("owner_name").Value.ToString();

            if (shotOwner.Equals(gameManager.LocalID)) {
                Debug.Log("We already handled this!");
                return;
            }

            LauncherShot shot = new LauncherShot();
            shot.Name = shotName;
            shot.OwnerName = shotOwner;
            shot.Owner = gameManager.GetLauncher(shot.OwnerName);

            string timeToImpact = data.Snapshot.Child("time_to_impact").Value.ToString();
            shot.TimeToImpact = float.Parse(timeToImpact);

            string powerSettingString = data.Snapshot.Child("power_setting").Value.ToString();
            shot.PowerSetting = float.Parse(powerSettingString);

            string startPosString = data.Snapshot.Child("start_pos").Value.ToString();
            string[] startPosArray = startPosString.Split(',');
            Vector3 startPos = new Vector3(
            float.Parse(startPosArray[0]),
            float.Parse(startPosArray[1]),
            float.Parse(startPosArray[2]));
            shot.StartPos = startPos;

            string velocityString = data.Snapshot.Child("velocity").Value.ToString();
            string[] velocityArray = velocityString.Split(',');
            Vector3 velocity = new Vector3(
            float.Parse(velocityArray[0]),
            float.Parse(velocityArray[1]),
            float.Parse(velocityArray[2]));
            shot.Velocity = velocity;

            string impactPosString = data.Snapshot.Child("impact_pos").Value.ToString();
            string[] impactArray = impactPosString.Split(',');
            Vector3 impactPos = new Vector3(
            float.Parse(impactArray[0]),
            float.Parse(impactArray[1]),
            float.Parse(impactArray[2]));
            shot.ImpactPos = impactPos;

            string doesHit = data.Snapshot.Child("does_hit").Value.ToString();
            shot.DoesHit = doesHit.Equals("true") ? true : false;
            string boardHit = data.Snapshot.Child("board_hit").Value.ToString();
            shot.BoardHit = boardHit.Equals("true") ? true : false;

            string hasDebris = data.Snapshot.Child("has_debris").Value.ToString();
            shot.HasDebris = hasDebris.Equals("true") ? true : false;

            shot.DebrisName = data.Snapshot.Child("debris_name").Value.ToString();

            string debrisOffsetString = data.Snapshot.Child("debris_offset").Value.ToString();
            string[] debrisOffsetArray = debrisOffsetString.Split(',');
            Vector3 debrisPos = new Vector3(
            float.Parse(debrisOffsetArray[0]),
            float.Parse(debrisOffsetArray[1]),
            float.Parse(debrisOffsetArray[2]));
            shot.DebrisOffset = debrisPos;

            gameManager.EnemyShot(shot);
        }
    }

    public void OnRestartChanged(object sender2, ValueChangedEventArgs data) {
        if (data.Snapshot.Exists) {
            bool status = false;
            if (data.Snapshot.Value.Equals(null)) {
                Debug.Log("Data is null");
                return;
            }
            status = data.Snapshot.Value.Equals("true");
            if(status) gameManager.ReEnterGame();
        }
    }


    public void UpdateIsland(string playerName, Vector3 pos, Quaternion rot, bool isReady, bool isPlaced) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players/" + playerName + "/island/");
        Dictionary<string, object> newLocationMap = new Dictionary<string, object>();

        float posX = (float)decimal.Round((decimal)pos.x, 3);
        float posY = (float)decimal.Round((decimal)pos.y, 3);
        float posZ = (float)decimal.Round((decimal)pos.z, 3);

        Vector3 euler = rot.eulerAngles;
        float rotX = (float)decimal.Round((decimal)euler.x, 3);
        float rotY = (float)decimal.Round((decimal)euler.y, 3);
        float rotZ = (float)decimal.Round((decimal)euler.z, 3);

        newLocationMap["ready"] = isReady;
        newLocationMap["placed"] = isPlaced;
        newLocationMap["localPosition"] = String.Format("{0},{1},{2}", posX, posY, posZ);
        newLocationMap["localRotation"] = String.Format("{0},{1},{2}", rotX, rotY, rotZ);

        reference.SetValueAsync(newLocationMap);
    }

    public void AddIslandListener(string playerName, EventHandler<ValueChangedEventArgs> callback) {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players/" + playerName + "/island").ValueChanged += callback;
    }

    public void RemoveIslandListener(string playerName, EventHandler<ValueChangedEventArgs> callback) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players/" + playerName + "/island");
        reference.ValueChanged -= callback;
        Debug.Log("Removing " + "rooms/" + thisRoomNumber + "/players/" + playerName + "/island");
        reference.RemoveValueAsync();
    }

    public void AddPlayerLauncher(string playerName, Vector3 pos, Vector3 rot) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players/" + playerName + "/catapult");

        Dictionary<string, object> newLocationMap = new Dictionary<string, object>();

        float posX = (float)decimal.Round((decimal)pos.x, 3);
        float posY = (float)decimal.Round((decimal)pos.y, 3);
        float posZ = (float)decimal.Round((decimal)pos.z, 3);

        float rotX = (float)decimal.Round((decimal)rot.x, 3);
        float rotY = (float)decimal.Round((decimal)rot.y, 3);
        float rotZ = (float)decimal.Round((decimal)rot.z, 3);

        newLocationMap["localPosition"] = String.Format("{0},{1},{2}", posX, posY, posZ);
        newLocationMap["localRotation"] = String.Format("{0},{1},{2}", rotX, rotY, rotZ);

        reference.SetValueAsync(newLocationMap);
    }

    public void AddPlayer(string playerName) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(
            "rooms/" + thisRoomNumber + "/players/" + playerName + "/connected");
        reference.SetValueAsync("true");
    }

    public void DisconnectPlayer(string playerName) {
        if (thisRoomNumber == -1)
            return;
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(
            "rooms/" + thisRoomNumber + "/players/" + playerName + "/connected");
        reference.SetValueAsync("false");
        SetRoomStatus(thisRoomNumber, "done");
    }

    public void ReconnectPlayer(string playerName) {
        if (thisRoomNumber == -1)
            return;
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("No game data");
            } else if (task.IsCompleted) {
                DataSnapshot data = task.Result;


                bool gameInProgress = gameManager.CurrentGameState == GameManager.GameState.Playing ||
                        gameManager.CurrentGameState == GameManager.GameState.Over;

                if (gameInProgress && (!data.HasChildren || data.ChildrenCount < 2)) {
                    Debug.Log("Can't join a game with less than 2 players");
                    gameManager.AbortGame("The other player has disconnected");
                    return;
                }

                if (!data.Child(playerName).Exists) {
                    gameManager.AbortGame("This game cannot be continued");
                    return;
                }


                DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(
                    "rooms/" + thisRoomNumber + "/players/" + playerName + "/connected");
                reference.SetValueAsync("true");                    

            }
        });      

        SetRoomStatus(thisRoomNumber, "done");
    }

    public void RemovePlayer(string playerName) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/players/" + playerName);
        reference.RemoveValueAsync();
        Debug.Log("Removing player " + playerName);
        SetRoomStatus(thisRoomNumber, "done");
    }

    public void UpdateRoomStatus(int roomToTest) {

        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomToTest + "/players").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("No game data");
            } else if (task.IsCompleted) {
                Debug.Log("Pinging Room");
                DataSnapshot data = task.Result;
                int numPlayers = 0;
                foreach (DataSnapshot player in data.Children) {
                    if (player.Child("connected").Exists && player.Child("connected").Value.Equals("true")) {
                        numPlayers++;
                    }
                }
                switch(numPlayers) {
                    case 0 :
                        ClearRoom(roomToTest);
                        Debug.Log("Room is empty clearing it out");
                        break;
                    case 1 :
                        Debug.Log("Room has one player");
                        SetRoomStatus(roomToTest, "waiting");
                        break;
                    case 2 :
                        Debug.Log("Room has two players");
                        SetRoomStatus(roomToTest, "full");
                        break;
                    default :
                        ClearRoom(roomToTest);
                        Debug.Log("Room is more that two players! WHAT?");
                        break;
 
                }
            }
        }); 
    }
 
    public void AddShot(LauncherShot shot) {
        shot.Name = Mathf.FloorToInt(Time.time * 1000).ToString();

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/shots/" + shot.Name);
        Debug.Log("Adding shot " + shot.Name + " from " + shot.Owner);
        reference.SetValueAsync(shot.GetData());
    }

    public void RemoveShot(int id) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/shots/" + id.ToString());
        reference.RemoveValueAsync();
        Debug.Log("Removing Shot");
    }

    public void SetRoomStatus(int roomNumber, string status) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomNumber + "/status");
        reference.SetValueAsync(status);     
    }

    public delegate void RoomStatusEvent(string status);
    public void GetRoomStatus(int roomNumber, RoomStatusEvent callback) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomNumber + "/status");
        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("No game data");
            } else if (task.IsCompleted) {
                DataSnapshot data = task.Result;
                callback(data.Value.ToString());
            }
        });
    }

    public void AddRoomStatusListener(int roomNumber, EventHandler<ValueChangedEventArgs> callback) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomNumber + "/status");
        reference.ValueChanged += callback;
        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("No game data");
            } else if (task.IsCompleted) {
                DataSnapshot data = task.Result;
                if (!data.Exists)
                    SetRoomStatus(roomNumber, "blank");
            }
        });
    }

    public void RemoveRoomStatusListener(int roomNumber, EventHandler<ValueChangedEventArgs> callback) {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomNumber + "/status").ValueChanged -= callback;
    }

    public delegate void PlayerNamesEvent(List<string> names);
    public void GetPlayerNames(int roomNumber, PlayerNamesEvent callback) {
        FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomNumber + "/players").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("No game data");
            } else if (task.IsCompleted) {
                IEnumerable <DataSnapshot> data = task.Result.Children;
                List<string> players = new List<string>();
                foreach (DataSnapshot player in data) {
                    players.Add(player.Value.ToString());
                }
                callback(players);
            }
        });       
    }

    public void SetRoomData(Dictionary<string, object> data) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber);
        reference.SetValueAsync(data);
    }
    public void SetRoomCloudId(string cloudID) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/cloud_id");
        reference.SetValueAsync(cloudID);
    }

    public void AddDebris(string debrisID) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/debris/" + debrisID);
        reference.SetValueAsync("alive");
    }

    public void ClearDebris(string debrisID) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/debris/" + debrisID);
        reference.SetValueAsync("cleared");
    }

    public void AddDebrisListener(string debrisID, EventHandler<ValueChangedEventArgs> callback) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/debris/" + debrisID);
        reference.ValueChanged += callback;
    }

    public void RemoveDebrisListener(string debrisID, EventHandler<ValueChangedEventArgs> callback) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/debris/" + debrisID);
        reference.ValueChanged -= callback;
    }

    public delegate void CloudIDEvent(string cloudID);
    public void GetRoomCloudId(int roomNumber, CloudIDEvent callback) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomNumber + "/cloud_id");
        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("No game data");
            } else if (task.IsCompleted) {
                DataSnapshot data = task.Result;
                callback(data.Value.ToString());
            }
        });
    }

    public void ClearShotsFromRoom() {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/shots");
        reference.RemoveValueAsync();       
    }

    public void ClearDebrisFromRoom() {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + thisRoomNumber + "/debris/");
        reference.RemoveValueAsync();
    }

    public void ClearRoom(int roomNumber) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rooms/" + roomNumber + "/players");
        reference.RemoveValueAsync();
        SetRoomStatus(roomNumber, "empty");
    }
}
