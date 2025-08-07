using UnityEngine;

public class YardSaleItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemName = "Item";
    [SerializeField] private float price = 10f;
    [SerializeField] private string prompt = "Press E to Buy";
    [SerializeField] private ItemCondition condition; // Assigned in Inspector

    public string GetInteractionPrompt()
    {
        string conditionName = condition != null ? condition.ConditionName : "Unknown";
        return $"{prompt} {itemName} (${price}, {conditionName})";
    }

    public void Interact(GameObject interactor)
    {
        PlayerWallet wallet = interactor.GetComponent<PlayerWallet>();
        PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
        WalletUI walletUI = FindObjectOfType<WalletUI>();

        if (wallet != null && inventory != null)
        {
            if (wallet.CanAfford(price))
            {
                wallet.SpendMoney(price);
                inventory.AddItem(new YardSaleItemData(itemName, price, condition));
                if (inventoryUI != null)
                {
                    inventoryUI.UpdateInventoryDisplay();
                }
                if (walletUI != null)
                {
                    walletUI.UpdateWalletDisplay();
                }
                Debug.Log($"Bought {itemName} (${price}, {condition?.ConditionName ?? "Unknown"}). Remaining money: ${wallet.GetMoney():F2}");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log($"Cannot afford {itemName} (${price}, {condition?.ConditionName ?? "Unknown"}). Current money: ${wallet.GetMoney():F2}");
            }
        }
        else
        {
            Debug.LogWarning("PlayerWallet or PlayerInventory component not found on interactor!");
        }
    }
}