using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance {get; private set;}

    [SerializeField]private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;


    private void Awake() {
        DeliveryManager.Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer += Time.deltaTime;
        if(spawnRecipeTimer >= spawnRecipeTimerMax){
            spawnRecipeTimer = 0;
            if(waitingRecipeSOList.Count < waitingRecipesMax){
                RecipeSO recipeSO = recipeListSO.recipeSOList[Random.Range(0,recipeListSO.recipeSOList.Count - 1)];
                waitingRecipeSOList.Add(recipeSO);
                Debug.Log(recipeSO.recipeName);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject){
        for(int i = 0;i < waitingRecipeSOList.Count; i++){
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count){
                //There are the same ingredients amount in the plate
                bool plateContentMatchRecipe = true;
                foreach(KitchenObjectSO waitingKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList){
                    //Cycling through all the recipe kitchen objects
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
                        if(plateKitchenObjectSO == waitingKitchenObjectSO){
                            //There's a match!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound){
                        //This plate did not give the right recipe
                        plateContentMatchRecipe = false;
                    }
                }
                if(plateContentMatchRecipe){
                    //Player delivered the correct recipe
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //Playre did not deliver the correct recipe
        Debug.Log("Playre did not deliver the correct recipe");
    }
}
