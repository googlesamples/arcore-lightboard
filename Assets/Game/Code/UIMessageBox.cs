//-----------------------------------------------------------------------
// <copyright file="UIMessageBox.cs" company="Google">
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

public class UIMessageBox : MonoBehaviour {

    bool isVisible = false;
    Text message;

    public bool IsVisible {
        get { return isVisible; }
        set {
            isVisible = value;
            Debug.Log("Setting is visible to " + isVisible);
            gameObject.SetActive(isVisible);
        }
    }

    Button okButton;

	public void Init () {
        message = transform.Find("Background/Message").GetComponent<Text>();
        okButton = transform.Find("Background/OKButton").GetComponent<Button>();
        okButton.onClick.AddListener(Close);
        IsVisible = false;
    }

    void Update () {

	}
    public void Open(string newMessage) {
        IsVisible = true;
        message.text = newMessage;
    }

    public void Close() {
        IsVisible = false;
    }

}
