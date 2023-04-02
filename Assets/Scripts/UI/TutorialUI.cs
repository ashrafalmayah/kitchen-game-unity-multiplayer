using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI moveUpText;
    [SerializeField]private TextMeshProUGUI moveDownText;
    [SerializeField]private TextMeshProUGUI moveLeftText;
    [SerializeField]private TextMeshProUGUI moveRightText;
    [SerializeField]private TextMeshProUGUI interactText;
    [SerializeField]private TextMeshProUGUI interactAlternateText;
    [SerializeField]private TextMeshProUGUI pauseText;
    [SerializeField]private TextMeshProUGUI gamepadInteractText;
    [SerializeField]private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField]private TextMeshProUGUI gamepadPauseText;

    private void Start() {
        GameInput.Instance.OnBindingRebound += GameInput_OnBindingReboud;
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;

        Show();

        UpdateVisual();
    }

    private void GameInput_OnBindingReboud(object sender, EventArgs e){
        UpdateVisual();
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e){
        if(GameManager.Instance.IsCountDownToStartActive()){
            Hide();
        }
    }

    private void UpdateVisual(){
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
