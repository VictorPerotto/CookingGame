using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress{

    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressBarChangedEventArgs> OnProgressBarChanged;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSO;

    int cuttingProgress = 0;

    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //clear counter is empty
            if(player.HasKitchenObject()){
                //player has kitchen object
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressBarChanged?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs{
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
        } else {
            //counter is not empty
            if(!player.HasKitchenObject()){
                //player is empty
                GetKitchenObject().SetKitchenObjectParent(player);

                float progressNow = 0;
                OnProgressBarChanged?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs{
                    progressNormalized = progressNow
                });
            }
        }
    }

    public override void InteractAlternate(Player player){
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            cuttingProgress ++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressBarChanged?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs{
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            
            if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){
                KitchenObjectSO outputKitchenObjectSO = GetInputForOutput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipesSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipesSO != null;
    }

    private KitchenObjectSO GetInputForOutput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipesSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if(cuttingRecipesSO.input != null){
            return cuttingRecipesSO.output;
        } else {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSO){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
