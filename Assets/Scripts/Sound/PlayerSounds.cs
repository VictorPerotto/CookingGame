using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private float footstepsTimerMax = 0.1f;
    private float footstepsTimer;
    private Player player;

    private void Start(){
        player = GetComponent<Player>();
    }

    private void Update(){
        footstepsTimer -= Time.deltaTime;
        
        if(footstepsTimer <= 0){
            footstepsTimer = footstepsTimerMax;

            if(player.IsWalking()){
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsAudio(player.transform.position, volume);
            }
        }
    }
}
