//-----------------------------------------------------------------------
// <copyright file="UIControl.cs" company="Google">
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



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    GameObject winScreen;
    Text winText;
    Button restartButton;
    Button quitButton;

    GameObject helpTextBox;
    Text helpTextLabel;

    float helpTimer = 2.0f;

    ProgressControl progress;
    UITipsDisplay tipsDisplay;
    UIMessageBox messageBox;

    UILobby lobby;

    AudioSource audioSource;
    public AudioSource AudioSource {
        get { return audioSource; }
    }

    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip buttonClickSound;
    public AudioClip[] countDownClickSounds;
    public AudioClip countDownFinishedSound;

    CountDownControl countDown;



    private void Start() {
        winScreen = transform.Find("WinScreen").gameObject;
        winText = transform.Find("WinScreen/WinMessage").GetComponent<Text>();
        restartButton = transform.Find("WinScreen/RestartButton").GetComponent<Button>();
        quitButton = transform.Find("WinScreen/QuitButton").GetComponent<Button>();

        messageBox = transform.Find("MessageBox").GetComponent<UIMessageBox>();
        messageBox.Init();


        countDown = transform.Find("CountDown").GetComponent<CountDownControl>();
        countDown.Init(this);
        winScreen.SetActive(false);

        helpTextBox = transform.Find("HelpText").gameObject;
        helpTextLabel = transform.Find("HelpText/Label").GetComponent<Text>();
        ClearHelpText();

        restartButton.onClick.AddListener(RestartButtonPressed);
        quitButton.onClick.AddListener(QuitButtonPressed);

        progress = transform.Find("Progress").GetComponent<ProgressControl>();
        tipsDisplay = transform.Find("Tips").GetComponent<UITipsDisplay>();

        lobby = transform.Find("LobbyUI").GetComponent<UILobby>();

        audioSource = Camera.main.gameObject.AddComponent<AudioSource>();

	}

    public void CloseIntro() {
        transform.Find("IntroUI").GetComponent<UIIntro>().IsVisible = false;
    }

	public UIControl Init() {
        return this;
    }

	private void Update() {
        if (helpTimer > 0) {
            helpTimer -= Time.deltaTime;
            if (helpTimer < 0)
                ClearHelpText();
        }
	}

    public void ShowWinScreen(string losersName) {
        winScreen.SetActive(true);

        bool isWinner = !losersName.Contains(GameManager.Instance.LocalID);
        string winMessage = isWinner ?
            "You Won!" : "You Lost";
        winText.text = winMessage;

        Invoke(isWinner ? "PlayWinSound" : "PlayLoseSound", 1.5f);
    }

    void PlayWinSound() {
        AudioSource.PlayOneShot(winSound);        
    }
    void PlayLoseSound() {
        AudioSource.PlayOneShot(loseSound);
    }

    public void HideWinScreen() {
        winScreen.SetActive(false);
    }

    public void RestartButtonPressed() {
        MultiplayerContol.Instance.SetRestart(true);
    }


    public void QuitButtonPressed() {
        Application.Quit();
    }

    public void ClearHelpText() {
        helpTextLabel.text = "";
        helpTextBox.SetActive(false);
    }

    public void ShowHelpText(string message, float timeToShow) {
        helpTextLabel.text = message;
        helpTimer = timeToShow;
        helpTextBox.SetActive(true);
    }
    
    public void ShowTips(bool setting) {
        tipsDisplay.IsVisible = setting;
    }

    public void ShowProgress(bool setting) {
        if (setting)
            progress.Open();
        else
            progress.Close();
    }


    public void SetScore(string player, int painted, int total) {
        Debug.Log("Setting hit on player " + player);
        if (painted >= total)
            ShowWinScreen(player);
    }

    public void ShowLobby(bool setting) {

        if (setting) {
            lobby.Open();
        } else {
            lobby.Close();
        }
    }

    public void PlayButtonSound() {
        AudioSource.PlayOneShot(buttonClickSound);
    }

    public void OpenCountDown() {
        countDown.IsVisible = true;
    }

    public void PlayClick(int index) {
        if (index >= countDownClickSounds.Length) return;

        AudioSource.PlayOneShot(countDownClickSounds[index], 0.5f);
    }

    public void PlayCountDownFinished() {
        AudioSource.PlayOneShot(countDownFinishedSound);
    }

    public void OpenMessageBox(string message) {
        messageBox.Open(message);
    }

}
