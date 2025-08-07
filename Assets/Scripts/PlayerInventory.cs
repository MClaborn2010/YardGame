using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<YardSaleItemData> items = new List<YardSaleItemData>();

    public void AddItem(YardSaleItemData item)
    {
        items.Add(item);
        string conditionName = item.Condition != null ? item.Condition.ConditionName : "Unknown";
        Debug.Log($"Added {item.Name} (${item.Price}, {conditionName}) to your inventory. Total items: {items.Count}");
    }

    public List<YardSaleItemData> GetItems()
    {
        return items;
    }
}