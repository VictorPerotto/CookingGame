using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter{
    
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private float platesSpawnedAmountMax;
    [SerializeField] private float plateSpawnTimerMax;
    [SerializeField] private KitchenObjectSO plateKitchenObjectoSO;

    private float platesSpawnedAmount;
    private float plateSpawnTimer;

    private void Update(){
        plateSpawnTimer += Time.deltaTime;

        if(plateSpawnTimer >= plateSpawnTimerMax){
            plateSpawnTimer = 0;

            if(platesSpawnedAmount < platesSpawnedAmountMax){
                platesSpawnedAmount ++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            if(platesSpawnedAmount > 0){
                KitchenObject.SpawnKitchenObject(plateKitchenObjectoSO, player);

                platesSpawnedAmount --;
                plateSpawnTimer = 0;

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
