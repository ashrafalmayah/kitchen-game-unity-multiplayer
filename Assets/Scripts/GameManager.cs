using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChanged;

    enum State{
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;


    private void Awake() {
        Instance = this;
    }

    private void Update() {
        switch(state){
            case State.WaitingToStart: 
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer <= 0){
                    state = State.CountDownToStart;
                    OnGameStateChanged?.Invoke(this , EventArgs.Empty);
                }
            break;
            case State.CountDownToStart: 
                countDownToStartTimer -= Time.deltaTime;
                if(countDownToStartTimer <= 0){
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnGameStateChanged?.Invoke(this , EventArgs.Empty);
                }
            break;
            case State.GamePlaying: 
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0){
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this , EventArgs.Empty);
                }
            break;
            case State.GameOver: 
            break;
        }
        Debug.Log(state);
    }

    public bool IsPlaying(){
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive(){
        return state == State.CountDownToStart;
    }

    public bool IsGameOver(){
        return state == State.GameOver;
    }

    public float GetCountDownToStartTimer(){
        return countDownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized(){
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
}
