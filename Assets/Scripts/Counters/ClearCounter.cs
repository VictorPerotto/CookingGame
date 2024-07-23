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
            if(player.HasKitchenObject()){
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    //this has a plate
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                //player is empty
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
