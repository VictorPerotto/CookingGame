using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour{
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameOptionsUI gameOptionsUI;

    private CanvasGroup canvasGroup;

    private void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();

        resumeButton.onClick.AddListener(() =>{
            GameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        optionsButton.onClick.AddListener(() =>{
            Hide();
            gameOptionsUI.Show(Show);
        });
    }

    private void Start(){
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e){
        Show();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e){
        Hide();
    }

    private void Show(){
        resumeButton.Select();

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    } 

    private void Hide(){
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
