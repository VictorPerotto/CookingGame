using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour{
    
    public static DeliveryManager Instance {get; private set;}

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
                RecipeSO WaitingRecipeSO = recipeSOlist.recipeSOList[Random.Range(0, recipeSOlist.recipeSOList.Count)];
                Debug.Log(WaitingRecipeSO.name);
                waitingRecipeSOList.Add(WaitingRecipeSO);
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
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    break;
                }
            }          

            Debug.Log("Player not delivered the correct recipe :()");  
        }
    }
}
