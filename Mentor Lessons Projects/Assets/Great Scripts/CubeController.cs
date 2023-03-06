using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    private Vector2 inputVector;
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        inputVector = callbackContext.ReadValue<Vector2>();
        
    }

    private void Update()
    {
       transform.Translate(new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime);
    }
}
