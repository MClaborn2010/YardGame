using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float maxPitch = 80f;
    [SerializeField] private Transform playerBody;

    private PlayerInputHandler inputHandler;
    private float xRotation;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 lookInput = inputHandler.LookInput * mouseSensitivity * Time.deltaTime;

        xRotation -= lookInput.y;
        xRotation = Mathf.Clamp(xRotation, -maxPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * lookInput.x);
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}