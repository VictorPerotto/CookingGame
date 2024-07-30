using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter{

    public static event EventHandler OnAnyTrash;

    new public static void ResetStaticData(){
        OnAnyTrash = null;
    }

    public override void Interact(Player player){
        if(player.HasKitchenObject()){
            OnAnyTrash?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }
    }
}
