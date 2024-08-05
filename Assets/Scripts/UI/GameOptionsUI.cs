using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour{

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    
    private CanvasGroup canvasGroup;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Button closeButton;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI moveUpButtonText;
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactAlternateButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;

    [SerializeField] private CanvasGroup pressKeyCanvasGroup;

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

        moveUpButton.onClick.AddListener(() => {RebindBinding(InputManager.Binding.MoveUp);});
        moveDownButton.onClick.AddListener(() => {RebindBinding(InputManager.Binding.MoveDown);});
        moveLeftButton.onClick.AddListener(() => {RebindBinding(InputManager.Binding.MoveLeft);});
        moveRightButton.onClick.AddListener(() => {RebindBinding(InputManager.Binding.MoveRight);});
        interactButton.onClick.AddListener(() => {RebindBinding(InputManager.Binding.Interact);});
        interactAlternateButton.onClick.AddListener(() => {RebindBinding(InputManager.Binding.InteractAlternate);});
        pauseButton.onClick.AddListener(() => {RebindBinding(InputManager.Binding.Pause);});

        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void UpdateVisuals(){
        moveUpButtonText.SetText(InputManager.Instance.GetBindingText(InputManager.Binding.MoveUp));
        moveDownButtonText.SetText(InputManager.Instance.GetBindingText(InputManager.Binding.MoveDown));
        moveLeftButtonText.SetText(InputManager.Instance.GetBindingText(InputManager.Binding.MoveLeft));
        moveRightButtonText.SetText(InputManager.Instance.GetBindingText(InputManager.Binding.MoveRight));
        interactButtonText.SetText(InputManager.Instance.GetBindingText(InputManager.Binding.Interact));
        interactAlternateButtonText.SetText(InputManager.Instance.GetBindingText(InputManager.Binding.InteractAlternate));
        pauseButtonText.SetText(InputManager.Instance.GetBindingText(InputManager.Binding.Pause));
    }

    private void Start(){
        InputManager.Instance.OnPauseAction += InputManager_OnPauseAction;
        
        UpdateVisuals();
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

    private void HideToPressKey(){
        pressKeyCanvasGroup.blocksRaycasts = false;
        pressKeyCanvasGroup.interactable = false;
        pressKeyCanvasGroup.alpha = 0f;
    }

    private void ShowToPressKey(){
        pressKeyCanvasGroup.blocksRaycasts = true;
        pressKeyCanvasGroup.interactable = true;
        pressKeyCanvasGroup.alpha = 1f;
    }

    private void RebindBinding(InputManager.Binding binding){
        ShowToPressKey();
        InputManager.Instance.RebindBinding(binding, () => {
            UpdateVisuals();
            HideToPressKey();
        });
    }
}
