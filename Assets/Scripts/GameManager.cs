using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public static GameManager Instance {get; private set;}
    
    private enum State{
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    } 

    public event EventHandler OnStateChanged;

    private float waitingToStartTimer = 1f;
    private float coutdownToStartTimer = 3f;
    private float gamePlayingTimer = 10f;
    private State state;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }

    private void Update(){
        switch(state){
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;

                if(waitingToStartTimer < 0){
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountDownToStart:
                coutdownToStartTimer -= Time.deltaTime;

                if(coutdownToStartTimer < 0){
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;

                if(gamePlayingTimer < 0){
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }

    public float GetCountdownToStart(){
        return coutdownToStartTimer;
    }

    public bool IsCountdownToStartActive(){
        return state == State.CountDownToStart;
    }

    public bool IsGamePlaying(){
        return state == State.GamePlaying;
    }
}
