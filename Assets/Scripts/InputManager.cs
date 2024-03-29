using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public sealed class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private PlayerInputActions playerInputActions;

    private PlayerInput playerInput;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        
        // If there is already an instance, then delete self;
        if (_instance != null && _instance != this)
        {
            Debug.Log("An instance of InputManager already exists in the scene.");
            Destroy(this.gameObject);
            return;
        }else{
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Lock cursor to middle of screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // This function is called when the object becomes enabled and active
    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    // This function is called when the behaviour becomes disabled
    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    // Returns the value of the player movement
    public Vector2 GetPlayerMovement()
    {
        return playerInput.actions["Movement"].ReadValue<Vector2>();

        // return playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    // Returns the value of the mouse movement
    public Vector2 GetPlayerMouseMovement()
    {
        return playerInputActions.Player.Look.ReadValue<Vector2>();
    }

    // Returns true if Jump button is pressed
    public bool PlayerJumped()
    {
        return playerInput.actions["Jump"].triggered;

        // return playerInputActions.Player.Jump.triggered;
    }

    // Returns true if Pause button is pressed
    public bool Pause()
    {
        return playerInputActions.UI.Pause.triggered;
    }
}
