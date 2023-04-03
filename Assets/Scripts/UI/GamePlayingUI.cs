using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingUI : MonoBehaviour
{
    [SerializeField]private Button pauseButton;
    
    private void Awake() {
        pauseButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });
    }
}
