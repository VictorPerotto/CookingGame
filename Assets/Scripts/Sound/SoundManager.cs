using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance {get; private set;}

    [SerializeField] private SoundsRefSO soundsRefSO;
    private float volume = 1f;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start(){
        TrashCounter.OnAnyTrash += TrashCounter_OnAnyTrash;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        DeliveryManager.Instance.OnRecipeSucceess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f){
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f){
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e){
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(soundsRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e){
        PlaySound(soundsRefSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e){
        PlaySound(soundsRefSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e){
        PlaySound(soundsRefSO.objectPickUp, Player.Instance.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e){
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(soundsRefSO.objectDrop, baseCounter.transform.position);
    }

    private void TrashCounter_OnAnyTrash(object sender, EventArgs e){
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(soundsRefSO.trash, trashCounter.transform.position);
    }

    public void PlayFootstepsAudio(Vector3 position, float volume){
        PlaySound(soundsRefSO.footsteps, position, volume * this.volume);
    }

    public void ChangeVolume(float volume){
        this.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }
}
