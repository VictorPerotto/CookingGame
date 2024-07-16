using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent{
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public ClearCounter secondClearCounter;
    public bool testing;

    private void Update(){
        if(testing && Input.GetKeyDown(KeyCode.T)){
            if(kitchenObject != null){
                kitchenObject.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }

    public void Interact(Player player){
        if(kitchenObject == null){
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else {
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public Transform GetKitchenObjectFollowTransform(){
        return counterTopPoint;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
}
