using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI recipesDeliveredText;
    [SerializeField]private Button restartButton;

    private void Awake() {
        restartButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
    }
    
    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        
        Hide();
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e){
        if(GameManager.Instance.IsGameOver()){
            Show();

            recipesDeliveredText.text = DeliveryManager.Instance.GetRecipesDeliveredSuccessfully().ToString();
        }else{
            Hide();
        }

    }

    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}
