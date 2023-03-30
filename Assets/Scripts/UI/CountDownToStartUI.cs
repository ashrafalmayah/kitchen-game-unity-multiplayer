using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CountDownToStartUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI countDownText;

    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        
        Hide();
    }

    private void Update() {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownToStartTimer()).ToString();
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e){
        if(GameManager.Instance.IsCountDownToStartActive()){
            Show();
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
