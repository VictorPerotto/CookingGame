using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour{

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisual;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake(){
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start(){
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e){
        Transform plateVisualTransform = Instantiate(plateVisual, counterTopPoint);

        float plateSpawnOffset = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateVisualGameObjectList.Count * plateSpawnOffset, 0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }


    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e){
        GameObject lastPlate = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(lastPlate);
        Destroy(lastPlate);
    }
}
