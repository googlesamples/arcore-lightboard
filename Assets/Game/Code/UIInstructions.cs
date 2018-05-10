//-----------------------------------------------------------------------
// <copyright file="UIInstructions.cs" company="Google">
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

public class UIInstructions : MonoBehaviour {

    public GameObject[] pages;

    Button nextButton;
    Button skipButton;
    Button doneButton;

    int currentPage = 0;

    void Start() {

        nextButton = transform.Find("Background/NextButton").GetComponent<Button>();
        nextButton.onClick.AddListener(Next);
        skipButton = transform.Find("Background/ExitButton").GetComponent<Button>();
        skipButton.onClick.AddListener(Close);
        doneButton = transform.Find("Background/DoneButton").GetComponent<Button>();
        doneButton.onClick.AddListener(Close);

        doneButton.gameObject.SetActive(false);
        SetPage(currentPage);

        Close();
    }

    public void Next() {
        currentPage++;
        SetPage(currentPage);
    }

    void SetPage(int pageNumber) {
        bool lastPage = pageNumber >= pages.Length - 1;
        nextButton.gameObject.SetActive(!lastPage);
        skipButton.gameObject.SetActive(!lastPage);
        doneButton.gameObject.SetActive(lastPage);

        for (int i = 0; i < pages.Length; i++) {
            pages[i].gameObject.SetActive(i == currentPage);
        }
    }
 
    public void Open() {
        currentPage = 0;
        SetPage(currentPage);
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}
