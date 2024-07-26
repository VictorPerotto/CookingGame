using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCountdownUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI gameCountdownText;

    private void Start(){
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e){
        if(GameManager.Instance.IsCountdownToStartActive()){
            Show();
        } else {
            Hide();
        }
    }

    private void Update(){
        if(GameManager.Instance.IsCountdownToStartActive()){
            gameCountdownText.SetText(Mathf.Ceil(GameManager.Instance.GetCountdownToStart()).ToString());
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }
    
    private void Hide(){
        gameObject.SetActive(false);
    }
}
