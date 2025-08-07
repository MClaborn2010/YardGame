using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool jumpInput;

    public Vector2 MoveInput => moveInput;
    public Vector2 LookInput => lookInput;
    public bool JumpInput => jumpInput;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }

    private void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        lookInput = inputActions.Player.Look.ReadValue<Vector2>();
        jumpInput = inputActions.Player.Jump.WasPressedThisFrame();
    }

    private void OnDestroy()
    {
        inputActions.Player.Disable();
    }
}