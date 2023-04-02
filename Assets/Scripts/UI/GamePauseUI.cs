using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField]private Button resumeButton;
    [SerializeField]private Button settingsButton;
    [SerializeField]private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        settingsButton.onClick.AddListener(() => {
            Hide();
            SettingsUI.Instance.Show(Show);
        });
    }

    private void Start() {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e){
        Show();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e){
        Hide();
    }

    private void Show(){
        gameObject.SetActive(true);

        resumeButton.Select();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
