using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }

    [SerializeField]private Button soundEffectsButton;
    [SerializeField]private Button musicButton;
    [SerializeField]private Button closeButton;
    [SerializeField]private TextMeshProUGUI soundEffectsText;
    [SerializeField]private TextMeshProUGUI musicText;

    private void Awake() {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();

            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            Hide();
        });
    }

    private void Start() {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        UpdateVisual();

        Hide();
    }

    private void UpdateVisual(){
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e){
        Hide();
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
