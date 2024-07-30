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

    [SerializeField]private float gamePlayingTimer = 10f;
    private float waitingToStartTimer = 1f;
    private float coutdownToStartTimer = 3f;
    private float gamePlayingTimerMax;
    private State state;

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    private bool gameIsPaused;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }

    private void Start(){
        InputManager.Instance.OnPauseAction += InputManager_OnPauseAction;

        gamePlayingTimerMax = gamePlayingTimer;
    }

    private void InputManager_OnPauseAction(object sender, EventArgs e){
        TogglePauseGame();
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
    }

    public void TogglePauseGame(){
        gameIsPaused = !gameIsPaused;

        if(gameIsPaused){
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetCountdownToStart(){
        return coutdownToStartTimer;
    }

    public bool IsCountdownToStartActive(){
        return state == State.CountDownToStart;
    }

    public bool IsGameOver(){
        return state == State.GameOver;
    }

    public bool IsGamePlaying(){
        return state == State.GamePlaying;
    }

    public float GetPlayingTimerNormalized(){
        return gamePlayingTimer / gamePlayingTimerMax;
    }
}
