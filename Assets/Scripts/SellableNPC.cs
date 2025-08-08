using UnityEngine;

public class SellableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string npcName = "Vendor";
    [SerializeField] private string prompt = "Press E to Sell";
    [SerializeField] public float resaleValue = 100f; // Percentage (e.g., 60 = 60%, 150 = 150%)

    public string GetInteractionPrompt()
    {
        return $"{prompt} to {npcName} ({resaleValue}% Base)";
    }

    public void Interact(GameObject interactor)
    {
        PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();
        PlayerWallet wallet = interactor.GetComponent<PlayerWallet>();
        InventoryUI inventoryUI = FindFirstObjectByType<InventoryUI>();
        WalletUI walletUI = FindFirstObjectByType<WalletUI>();

        if (inventory != null && inventory.GetItems().Count > 0)
        {
            var item = inventory.GetItems()[0];
            float conditionMultiplier = item.Condition != null ? item.Condition.PriceMultiplier : 1f;
            float sellPrice;
            if (item.Name == "Trash") // Use == for comparison
            {
                sellPrice = 1;
            }
            else
            {
                sellPrice = item.Price * (resaleValue / 100f) * conditionMultiplier;
            }
            inventory.GetItems().RemoveAt(0);
            wallet.SpendMoney(-sellPrice); // Add sell price to wallet
            string conditionName = item.Condition != null ? item.Condition.ConditionName : "Unknown";
            if (inventoryUI != null) inventoryUI.UpdateInventoryDisplay();
            if (walletUI != null) walletUI.UpdateWalletDisplay();
            Debug.Log($"Sold {item.Name} (${item.Price}, {conditionName}) for ${sellPrice:F2} (Resale: {resaleValue}%, Condition: {conditionMultiplier}x). New money: ${wallet.GetMoney():F2}");
        }
        else
        {
            Debug.Log("No items to sell!");
        }
    }
}