using System;

[Serializable]
public class YardSaleItemData
{
    public string Name;
    public float Price;
    public ItemCondition Condition;

    public YardSaleItemData(string name, float price, ItemCondition condition)
    {
        Name = name;
        Price = price;
        Condition = condition;
    }
}