using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerOld : MonoBehaviour
{
    private Rigidbody playerRb;
    PlayerInputActions playerInputActions;


    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        float speed = 5f;
        playerRb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump pressed: " + context.phase);
        
        playerRb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
    }

    // OnDestroy is called when the scene ends
    private void OnDestroy(){
        Debug.Log("OnDestroy called");
        playerInputActions.Player.Jump.performed -= Jump;
    }

}
