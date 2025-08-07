using UnityEngine;
using TMPro;
using System.Text;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private TextMeshProUGUI interactionPromptText;

    private void Start()
    {
        UpdateInventoryDisplay();
        ClearInteractionPrompt();
    }

    public void UpdateInventoryDisplay()
    {
        if (playerInventory == null || inventoryText == null)
        {
            Debug.LogWarning("PlayerInventory or InventoryText not assigned in InventoryUI!");
            return;
        }

        List<YardSaleItemData> items = playerInventory.GetItems();
        StringBuilder sb = new StringBuilder("Inventory:\n");
        if (items.Count == 0)
        {
            sb.Append("Empty");
        }
        else
        {
            foreach (YardSaleItemData item in items)
            {
                string conditionName = item.Condition != null ? item.Condition.ConditionName : "Unknown";
                sb.AppendLine($"{item.Name} (${item.Price}, {conditionName})");
            }
        }
        inventoryText.text = sb.ToString();
    }

    public void ShowInteractionPrompt(string prompt)
    {
        if (interactionPromptText != null)
        {
            interactionPromptText.text = prompt;
            Debug.Log($"Showing prompt: {prompt}");
        }
    }

    public void ClearInteractionPrompt()
    {
        if (interactionPromptText != null)
        {
            interactionPromptText.text = "";
            Debug.Log("Cleared interaction prompt");
        }
    }
}