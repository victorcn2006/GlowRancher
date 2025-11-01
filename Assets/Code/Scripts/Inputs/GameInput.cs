using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private Controls playerInputActions;
    private void Awake() {
        playerInputActions = new Controls();
        playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
