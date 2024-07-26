using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    public static SoundManager Instance;

    [SerializeField] private SoundsRefSO soundsRefSO;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }

    private void Start(){
        TrashCounter.OnAnyTrash += TrashCounter_OnAnyTrash;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        DeliveryManager.Instance.OnRecipeSucceess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f){
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f){
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
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
        PlaySound(soundsRefSO.footsteps, position, volume);
    }
}
