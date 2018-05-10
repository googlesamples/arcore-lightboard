//-----------------------------------------------------------------------
// <copyright file="LauncherShot.cs" company="Google">
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LauncherShot {
    string _name;
    LauncherControl _owner;
    string _ownerName;
    float _timeToImpact;
    float _powerSetting;
    Vector3 _startPos;
    Vector3 _velocity;
    Vector3 _impactPos;
    bool _doesHit;
    bool _boardHit;
    bool _isReplay = false;
    bool _hasDebris = false;
    string _debrisName;
    Vector3 _debrisOffset = Vector3.zero;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public LauncherControl Owner {
        get { return _owner; }
        set { _owner = value; }
    }

    public string OwnerName {
        get { return _ownerName; }
        set { _ownerName = value; }
    }

    public Vector3 Velocity {
        get { return _velocity; }
        set { _velocity = value; }
    }

    public float PowerSetting {
        get { return _powerSetting; }
        set { _powerSetting = value; }
    }

    public Vector3 StartPos {
        get { return _startPos; }
        set { _startPos = value; }
    }

    public bool DoesHit {
        get { return _doesHit; }
        set { _doesHit = value; }
    }

    public Vector3 ImpactPos {
        get { return _impactPos; }
        set { _impactPos = value; }
    }

    public float TimeToImpact {
        get { return _timeToImpact; }
        set { _timeToImpact = value; }
    }

    public bool BoardHit {
        get { return _boardHit; }
        set { _boardHit = value; }
    }

    public bool IsReplay {
        get { return _isReplay; }
        set { _isReplay = value; }
    }

    public bool HasDebris {
        get { return _hasDebris; }
        set { _hasDebris = value; }
    }

    public string DebrisName {
        get { return _debrisName; }
        set { _debrisName = value; }
    }

    public Vector3 DebrisOffset {
        get { return _debrisOffset; }
        set { _debrisOffset = value; }
    }

    public LauncherShot() {
    }

    public LauncherShot(string name, LauncherControl owner, Vector3 startPos, Vector3 velocity) {
        this._name = name;
        this._owner = owner;
        this._ownerName = _owner.PlayerName;
        this._startPos = startPos;
        this._velocity = velocity;

        HasDebris = UnityEngine.Random.value < 0.6f;

        DebrisName = "debris" + Mathf.FloorToInt(UnityEngine.Random.value * 100000).ToString();
        DebrisOffset = new Vector3(
            UnityEngine.Random.Range(-0.1f, 0.1f),
            UnityEngine.Random.Range(0.5f, 0.7f),
            UnityEngine.Random.Range(0.0f, 0.3f));
    }

    public Dictionary<string, object> GetData() {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("name", _name);
        data.Add("owner_name", _ownerName);

        float timeToImpact = (float)decimal.Round((decimal)_timeToImpact, 3);
        data.Add("time_to_impact", String.Format("{0}", timeToImpact));

        float posX = (float)decimal.Round((decimal)_startPos.x, 3);
        float posY = (float)decimal.Round((decimal)_startPos.y, 3);
        float posZ = (float)decimal.Round((decimal)_startPos.z, 3);
        data.Add("start_pos", String.Format("{0},{1},{2}", posX, posY, posZ));

        data.Add("power_setting", String.Format("{0}", _powerSetting));

        float velX = (float)decimal.Round((decimal)_velocity.x, 3);
        float velY = (float)decimal.Round((decimal)_velocity.y, 3);
        float velZ = (float)decimal.Round((decimal)_velocity.z, 3);
        data.Add("velocity", String.Format("{0},{1},{2}", velX, velY, velZ));

        float impactX = (float)decimal.Round((decimal)_impactPos.x, 3);
        float impactY = (float)decimal.Round((decimal)_impactPos.y, 3);
        float impactZ = (float)decimal.Round((decimal)_impactPos.z, 3);
        data.Add("impact_pos", String.Format("{0},{1},{2}", impactX, impactY, impactZ));

        data.Add("does_hit", _doesHit ? "true" : "false");
        data.Add("board_hit", _boardHit ? "true" : "false");

        data.Add("has_debris", _hasDebris ? "true" : "false");
        data.Add("debris_name", _debrisName);

        float offsetX = (float)decimal.Round((decimal)_debrisOffset.x, 3);
        float offsetY = (float)decimal.Round((decimal)_debrisOffset.y, 3);
        float offsetZ = (float)decimal.Round((decimal)_debrisOffset.z, 3);
        data.Add("debris_offset", String.Format("{0},{1},{2}", offsetX, offsetY, offsetZ));

        data.Add("timestamp", ServerValue.Timestamp);

        return data;
    }

    public Vector3 Evaluate(float time) {
        Vector3 pos = _startPos;
        pos += (_velocity * time) + (new Vector3(0, -1, 0) * time * time);
        return pos;
    }
}

