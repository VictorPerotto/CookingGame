using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour{

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    PlayerInputActions playerInputActions;

    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Interact.performed += Interact_performed;
    }
    
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        
        return inputVector;
    }
}
