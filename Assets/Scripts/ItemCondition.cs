using UnityEngine;

[CreateAssetMenu(fileName = "New Item Condition", menuName = "YardSale/Item Condition")]
public class ItemCondition : ScriptableObject
{
    [SerializeField] private string conditionName = "Good";
    [SerializeField] private float priceMultiplier = 1f; // E.g., 1 for Good, 0.5 for Poor, 1.5 for Mint

    public string ConditionName => conditionName;
    public float PriceMultiplier => priceMultiplier;
}