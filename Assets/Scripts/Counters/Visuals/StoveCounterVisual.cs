using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour{
    
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject visual;

    private void Start(){
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e){
        if(e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried){
            particles.SetActive(true);
            visual.SetActive(true);
        } else{
            particles.SetActive(false);
            visual.SetActive(false);
        }
    }
}
