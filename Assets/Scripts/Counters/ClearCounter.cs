using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //Counter doesn't have anything
            if(player.HasKitchenObject()){
                //Player is carrying an object
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
}