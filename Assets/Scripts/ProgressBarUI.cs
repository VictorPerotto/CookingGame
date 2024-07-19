using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour{

    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image barImage;
    private IHasProgress hasProgress;

    private void Start(){
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        hasProgress.OnProgressBarChanged += IHasProgress_OnProgressBarChanged;

        Hide();
    }   

    private void IHasProgress_OnProgressBarChanged(object sender, IHasProgress.OnProgressBarChangedEventArgs e){
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
