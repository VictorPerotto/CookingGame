using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour{

    private bool onFirstUpdate = true;

    private void Update(){
        if(onFirstUpdate){
            onFirstUpdate = false;

            Loader.LoaderCallback();
        }
    } 
}
