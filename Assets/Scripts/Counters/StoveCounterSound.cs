using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private bool playWarningSound;
    private float warningSoundTimer;
    private float warningSoundTimerMax = .2f;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update() {
        if(playWarningSound){
            warningSoundTimer -= Time.deltaTime;
            if(warningSoundTimer < 0f){
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEvenArgs e){
        float burnShowProgressAmount = .5f;
        playWarningSound = e.ProgressNormalized > burnShowProgressAmount && stoveCounter.IsFried();
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventsArgs e){
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(playSound){
            audioSource.Play();
        }else{
            audioSource.Pause();
        }
    }
}
