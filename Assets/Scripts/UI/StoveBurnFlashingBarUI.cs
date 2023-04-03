using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField]private StoveCounter stoveCounter;
    private Animator animator;
    private bool show;

    private void Awake() {
        animator = GetComponent<Animator>();

        animator.SetBool(IS_FLASHING , false);
    }

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEvenArgs e){
        float burnShowProgressAmount = .5f;
        show = e.ProgressNormalized > burnShowProgressAmount && stoveCounter.IsFried();

        animator.SetBool(IS_FLASHING , show);
    }
}
