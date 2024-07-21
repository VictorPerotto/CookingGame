using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject{

    [SerializeField] private List<KitchenObjectSO> validIngredientsList;

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs{
        public KitchenObjectSO kitchenObjectSO;
    }

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake(){
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        if(!validIngredientsList.Contains(kitchenObjectSO)){
            return false;
        }

        if(kitchenObjectSOList.Contains(kitchenObjectSO)){
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }   

    public List<KitchenObjectSO> GetKitchenObjectSOList(){
        return kitchenObjectSOList;
    }
}
