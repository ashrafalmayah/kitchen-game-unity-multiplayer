using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]private CuttingRecipeSO[] CuttingRecipeSOArray; 

    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //Counter doesn't have anything
            if(player.HasKitchenObject()){
                //Player is carrying an object
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Can be placed to cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else{
            //Counter has an object above it
            if(!player.HasKitchenObject()){
                //Player isn't carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player){
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
            //There is a kitchen object AND can be cut
            KitchenObjectSO outputKitchenObjectSO = GetInputForOutput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO,this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (CuttingRecipeSO cuttingRecipeSO in CuttingRecipeSOArray){
            if(inputKitchenObjectSO == cuttingRecipeSO.input){
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO GetInputForOutput(KitchenObjectSO input){
        foreach (CuttingRecipeSO cuttingRecipeSO in CuttingRecipeSOArray){
            if(input == cuttingRecipeSO.input){
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
