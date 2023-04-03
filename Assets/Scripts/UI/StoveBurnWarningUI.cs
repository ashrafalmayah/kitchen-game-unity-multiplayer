using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;
    [SerializeField]private Image warningImage;

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEvenArgs e){
        float burnShowProgressAmount = .5f;
        bool show = e.ProgressNormalized > burnShowProgressAmount && stoveCounter.IsFried();
        if(show){
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
