using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour{

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    
    private CanvasGroup canvasGroup;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Button closeButton;

    private void Awake(){

        soundEffectsSlider.value = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);

        closeButton.onClick.AddListener(() => {
            Hide();
        });

        soundEffectsSlider.onValueChanged.AddListener(delegate {
            SoundManager.Instance.ChangeVolume(soundEffectsSlider.value);
        });

        musicSlider.onValueChanged.AddListener(delegate {
            MusicManager.Instance.ChangeVolume(musicSlider.value);
        });

        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start(){
        InputManager.Instance.OnPauseAction += InputManager_OnPauseAction;
        
        Hide();
    }

    private void InputManager_OnPauseAction(object sender, EventArgs e){
        Hide();
    }

    public void Show(){
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
    }

    private void Hide(){
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;
    }
}
