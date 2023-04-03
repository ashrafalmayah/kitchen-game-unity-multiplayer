using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField]private Image backgroundImage;
    [SerializeField]private Image iconImage;
    [SerializeField]private TextMeshProUGUI messageText;
    [SerializeField]private Color successColor;
    [SerializeField]private Color failedColor;
    [SerializeField]private Sprite successSprite;
    [SerializeField]private Sprite failSprite;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;

        Hide();
    }

    private void DeliveryManager_OnDeliveryFail(object sender, EventArgs e){
        Show();
        
        animator.SetTrigger(POPUP);

        backgroundImage.color = failedColor;
        iconImage.sprite = failSprite;
        messageText.text = "Delivery\nFailed";
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, EventArgs e){
        Show();

        animator.SetTrigger(POPUP);

        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "Delivery\nSuccess";
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

}
