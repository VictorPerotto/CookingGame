using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour{
    
    public static DeliveryManager Instance {get; private set;}

    public event EventHandler OnRecipeSucceess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    [SerializeField] RecipeListSO recipeSOlist;
    [SerializeField] private float spawnRecipeTimerMax;
    [SerializeField] private float waitingRecipeMax;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update(){
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer <= 0){
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax){
                RecipeSO WaitingRecipeSO = recipeSOlist.recipeSOList[UnityEngine.Random.Range(0, recipeSOlist.recipeSOList.Count)];
                waitingRecipeSOList.Add(WaitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
        
        for(int i=0; i<waitingRecipeSOList.Count; i++){
            
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count){
                bool plateContentsMatchesRecipe = true;

                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList){
                    bool ingredientFound = false;

                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){

                        if(plateKitchenObjectSO == recipeKitchenObjectSO){
                            ingredientFound = true;
                            break;
                        }
                    }

                    if(!ingredientFound){
                        plateContentsMatchesRecipe = false;
                    }
                }

                if(plateContentsMatchesRecipe){
                    Debug.Log("Success");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSucceess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }          
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList(){
        return waitingRecipeSOList;
    }
}
