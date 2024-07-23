using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter{

    public override void Interact(Player player){
        if(player.HasKitchenObject()){
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                if(plateKitchenObject.GetKitchenObjectSOList().Count > 0){
                    DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

                    plateKitchenObject.DestroySelf();
                }
            }
        }
    }
}
