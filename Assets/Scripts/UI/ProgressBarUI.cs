using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]private Image barImage;
    [SerializeField]private GameObject hasProgressGameObject;

    private IHasProgress hasProgress;

    private void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null){
            Debug.LogError("GameObject : " + hasProgressGameObject + " doesn't have a IHasProgress component!");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEvenArgs e)
    {
        barImage.fillAmount = e.ProgressNormalized;
        if(barImage.fillAmount == 0 || barImage.fillAmount == 1){
            Hide();
        }else{
            Show();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
