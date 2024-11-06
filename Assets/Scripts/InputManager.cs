using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager


    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public Vector2 movementInput;
    public float moveAmount;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    public bool isPauseKeyPressed = false;

    public InteractionManager interactionManager;

    public PlayerInputActions playerControls;
    private InputAction actionMove;
    private InputAction actionLook;
    private InputAction actionJump;
    private InputAction actionSprint;
    private InputAction actionFire;
    private void OnEnable() {
        playerControls = new PlayerInputActions();
        interactionManager = FindObjectOfType<InteractionManager>();
        actionMove = playerControls.Player.Move;
        actionLook = playerControls.Player.Look;
        actionJump = playerControls.Player.Jump;
        actionSprint = playerControls.Player.Sprint;
        actionFire = playerControls.Player.Fire;
        actionMove.Enable();
        actionLook.Enable();
        actionJump.Enable();
        actionSprint.Enable();
        actionFire.Enable();
    }
    private void OnDisable() {
        actionMove.Disable();
        actionLook.Disable();
        actionJump.Disable();
        actionSprint.Disable();
        actionFire.Disable();
    }
    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleCameraInput();
        HandlePauseKeyInput();
        HandleInteractionInput();
    }

    private void HandleCameraInput()
    {        
            // Get mouse input for the camera
          //  cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraInput = actionLook.ReadValue<Vector2>();

        // Get scroll input for camera zoom
        scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Send inputs to CameraManager
            cameraManager.zoomInput = scrollInput;
            cameraManager.cameraInput = cameraInput;        
    }

    private void HandleMovementInput()
    {
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementInput = actionMove.ReadValue<Vector2>();

  
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); // Detect the escape key press
    }

    private void HandleSprintingInput()
    {
        if (actionSprint.IsPressed() && moveAmount > 0.5f)
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleInteractionInput()
    {
        if (actionFire.IsPressed() && interactionManager.interactionPossible)
        {
            interactionManager.Interact();
        }
    }

    private void HandleJumpInput()
    {
        //  jumpInput = Input.GetKeyDown(KeyCode.Space); // Detect jump input (spacebar)
        jumpInput = actionJump.IsPressed();
        if (jumpInput)
        {
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
        }
    }






}
