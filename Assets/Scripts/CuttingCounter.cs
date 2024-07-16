using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter{

    public event EventHandler OnCut;

    [SerializeField] private KitchenObjectSO slicedObjectSO;

    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //clear counter is empty
            if(player.HasKitchenObject()){
                //player has kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            //counter is not empty
            if(!player.HasKitchenObject()){
                //player is empty
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player){
        if(HasKitchenObject()){
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(slicedObjectSO, this);
            OnCut?.Invoke(this, EventArgs.Empty);
        }
    }
}
