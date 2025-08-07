using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class InteractionManager : MonoBehaviour
{
    [SerializeField] private float interactionRange = 4f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private InventoryUI inventoryUI;

    private PlayerInputHandler inputHandler;
    private IInteractable currentInteractable;
    private IInteractable lastDetectedInteractable;
    private int hitConfirmationCount = 0;
    private int missConfirmationCount = 0;
    private const int confirmationThreshold = 3; 
    private Camera mainCamera;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        IInteractable newInteractable = null;
        string newPrompt = "";

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer))
        {
            newInteractable = hit.collider.GetComponent<IInteractable>();
            if (newInteractable != null)
            {
                newPrompt = newInteractable.GetInteractionPrompt();
            }
        }

        if (newInteractable == lastDetectedInteractable)
        {
            if (newInteractable != null)
            {
                hitConfirmationCount++;
                missConfirmationCount = 0;
            }
            else
            {
                missConfirmationCount++;
                hitConfirmationCount = 0;
            }
        }
        else
        {
            lastDetectedInteractable = newInteractable;
            hitConfirmationCount = newInteractable != null ? 1 : 0;
            missConfirmationCount = newInteractable == null ? 1 : 0;
        }

        // Update UI only after confirmation
        if (hitConfirmationCount >= confirmationThreshold && currentInteractable != lastDetectedInteractable)
        {
            currentInteractable = lastDetectedInteractable;
            inventoryUI?.ShowInteractionPrompt(newPrompt);
        }
        else if (missConfirmationCount >= confirmationThreshold && currentInteractable != null)
        {
            currentInteractable = null;
            inventoryUI?.ClearInteractionPrompt();
        }

        // Handle interaction
        if (currentInteractable != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact(gameObject);
            inventoryUI?.UpdateInventoryDisplay();
            currentInteractable = null;
            inventoryUI?.ClearInteractionPrompt();
            hitConfirmationCount = 0;
            missConfirmationCount = 0;
        }
    }
}