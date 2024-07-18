using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour{

    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image barImage;

    private void Start(){
        cuttingCounter.OnProgressBarChanged += CuttingCounter_OnProgressBarChanged;

        Hide();
    }   

    private void CuttingCounter_OnProgressBarChanged(object sender, CuttingCounter.OnProgressBarChangedEventArgs e){
        if(e.progressNormalized == 0 || e.progressNormalized == 1){
            Hide();
        } else {
            Show();
            barImage.fillAmount = e.progressNormalized;
        }
        
    }

    private void Show(){
        canvasGroup.alpha = 1;
    }

    private void Hide(){
        canvasGroup.alpha = 0;
    }
}
