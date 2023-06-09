using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress {
    public event EventHandler<IHasProgress.OnProgressChangedEvenArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventsArgs> OnStateChanged;
    public class OnStateChangedEventsArgs : EventArgs{
        public State state;
    }

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField]private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField]private BurningRecipeSO[] burningRecipeSOArray;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private float fryingTimer;
    private float burningTimer;

    private State state;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if(HasKitchenObject()){
            switch(state){
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this , new IHasProgress.OnProgressChangedEvenArgs {
                        ProgressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if(fryingTimer > fryingRecipeSO.fryingTimerMax){
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs{
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this , new IHasProgress.OnProgressChangedEvenArgs {
                        ProgressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if(burningTimer > burningRecipeSO.burningTimerMax){
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnProgressChanged?.Invoke(this , new IHasProgress.OnProgressChangedEvenArgs {
                            ProgressNormalized = 0f
                        });
                        OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs{
                            state = state
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //Counter doesn't have anything
            if(player.HasKitchenObject()){
                //Player is carrying an object
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Can be placed to fry
                    //Drop!!
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingTimer = 0;
                    state = State.Frying;
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this , new IHasProgress.OnProgressChangedEvenArgs {
                        ProgressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs{
                        state = state
                    });
                }
            }
        }
        else{
            //Counter has an object above it
            if(!player.HasKitchenObject()){
                //Player isn't carrying anything
                //Pick Up!!
                GetKitchenObject().SetKitchenObjectParent(player);
                
                state = State.Idle;

                OnProgressChanged?.Invoke(this , new IHasProgress.OnProgressChangedEvenArgs {
                    ProgressNormalized = 0
                });
                OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs{
                    state = state
                });
            }else{
                //Player is carrying something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //Player is carrying a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;

                        OnProgressChanged?.Invoke(this , new IHasProgress.OnProgressChangedEvenArgs {
                            ProgressNormalized = 0
                        });
                        OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs{
                            state = state
                        });
                    }
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetInputForOutput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if(inputKitchenObjectSO == fryingRecipeSO.input){
            return fryingRecipeSO.output;
        }else{
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray){
            if(inputKitchenObjectSO == fryingRecipeSO.input){
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray){
            if(inputKitchenObjectSO == burningRecipeSO.input){
                return burningRecipeSO;
            }
        }
        return null;
    }

    public bool IsFried(){
        return state == State.Fried;
    }
}
