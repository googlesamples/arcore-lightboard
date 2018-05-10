//-----------------------------------------------------------------------
// <copyright file="GameBoard.cs" company="Google">
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


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameBoard : MonoBehaviour {

    Island island;

    public GameObject tilePrefab;

    Vector3 gridSize = new Vector3(1.0f, 1.0f, 1.0f);

    public Vector3 GridSize {
        get { return gridSize; }
    }

    GameObject board;
    Transform startPos;
    public Transform StartPose {
        get { return startPos; }
    }
    Transform readyPos;
    public Transform ReadyPose {
        get { return readyPos; }
    }

    Vector3 gridDivisions = new Vector3(10, 1, 10);

    [System.Serializable]
    public class GridPos {
        public Vector3 position {
            get { return cube.transform.localPosition; }
            set { cube.transform.localPosition = value; }
        }
        public GameObject cube;
    }

    public Vector3 CubeSize {
        get { return new Vector3(GridSize.x/gridDivisions.x, GridSize.y, GridSize.z / gridDivisions.z); }
    }

    bool initReady = false;
    public UnityEvent terrainReady = new UnityEvent();

    public AnimationCurve terrainEntranceCurve;
    float entranceTimer = 0.0f;
    float entranceDuration = 0.5f;

    List<GameObject> tiles =  new List<GameObject>();

    public Material player1Mat;
    public Material player2Mat;

    public void Init() {
        island = transform.parent.GetComponent<Island>();
        gridSize = GameManager.Instance.GridSize;
        gridDivisions = GameManager.Instance.GridDivisions;

        GenerateBoard();

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        initReady = true;
    }

    void GenerateBoard() {

        board = Instantiate(GameManager.Instance.boardPrefab, Vector3.up * 10, Quaternion.identity, transform);
        board.transform.localRotation = Quaternion.identity;
        Transform boardRoot = board.transform.Find("Board");
        boardRoot.GetComponent<Renderer>().material = island.PlayerNumber == 1 ? player1Mat : player2Mat;
        startPos = boardRoot.transform.Find("StartPoint");
        readyPos = boardRoot.transform.Find("ReadyPoint");
        Transform tileSpotGroup = boardRoot.transform.Find("TileSpots");
        GameObject tileGroup = new GameObject("GameTiles");
        tileGroup.transform.SetParent(boardRoot);
        tileGroup.transform.localPosition = tileGroup.transform.localPosition;
        tileGroup.transform.localRotation = tileGroup.transform.localRotation;
            
        int tileNumber = 0;
        foreach (Transform tileSpot in tileSpotGroup) {
            tileNumber++;
            GameObject tile = Instantiate(tilePrefab, tileSpot.position, tileSpot.rotation, tileGroup.transform);
            tiles.Add(tile);
            tile.name = "Player1" + tileNumber + "-Clean";
            tile.tag = "Tiles";
            foreach (Transform child in tile.transform) {
                child.gameObject.SetActive(false);
            }
        }
        Destroy(tileSpotGroup.gameObject);
    }

    void Update() {

        if (!initReady)
            return;

        if (entranceTimer < 1.0f) {
            entranceTimer += Time.deltaTime / entranceDuration;
            float amount = terrainEntranceCurve.Evaluate(entranceTimer);
            transform.localScale = Vector3.Lerp(Vector3.one * 0.05f, Vector3.one, amount);
            board.transform.localPosition = Vector3.zero;

            if (entranceTimer > 1.0f)
                terrainReady.Invoke();
        }
    }

    public void CheckScore() {
        int totalCount = tiles.Count;
        int paintedCount = 0;
        foreach (GameObject tile in tiles) {
            if (tile.name.Contains("Painted"))
                paintedCount++;
        }
        GameManager.Instance.SetScore(island.PlayerName, paintedCount, totalCount);
    }
}
