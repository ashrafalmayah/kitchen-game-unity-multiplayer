using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    [SerializeField]private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int spawnedPlatesAmount;
    private int spawnedPlatesAmountMax = 4;

    private void Update() {
        if(GameManager.Instance.IsPlaying() && spawnedPlatesAmount < spawnedPlatesAmountMax){
            spawnPlateTimer += Time.deltaTime;
            if(spawnPlateTimer > spawnPlateTimerMax){
                spawnPlateTimer = 0;
                //Spawn a new plate
                spawnedPlatesAmount++;
                OnPlateSpawned?.Invoke(this , EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject()){
            //Player isn't carrying anything
            if(spawnedPlatesAmount > 0){
                //There is a plate in the counter
                spawnedPlatesAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO , player);
                OnPlateRemoved?.Invoke(this , EventArgs.Empty);
            }
        }
    }
}
