using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter{

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
}
