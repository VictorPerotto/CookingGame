using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter{

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSO;

    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //clear counter is empty
            if(player.HasKitchenObject()){
                //player has kitchen object
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
            KitchenObjectSO outputKitchenObjectSO = GetInputForOutput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            OnCut?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSO){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO GetInputForOutput(KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSO){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
