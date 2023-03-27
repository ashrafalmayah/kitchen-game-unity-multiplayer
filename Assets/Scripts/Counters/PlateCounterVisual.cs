using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField]private PlatesCounter platesCounter;
    [SerializeField]private Transform counterTopPoint;
    [SerializeField]private Transform plateVisualPrefab;
    private List<GameObject> plateVisualGameObjectList;

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e) {
        GameObject removedPlate = plateVisualGameObjectList[plateVisualGameObjectList.Count-1];
        plateVisualGameObjectList.Remove(removedPlate);
        Destroy(removedPlate);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e) {
        Transform spawnedPlate = Instantiate(plateVisualPrefab,counterTopPoint);

        float plateOffsetY = 0.1f;
        spawnedPlate.localPosition = new Vector3(0f , plateOffsetY * plateVisualGameObjectList.Count , 0f );

        plateVisualGameObjectList.Add(spawnedPlate.gameObject);
    }
}
