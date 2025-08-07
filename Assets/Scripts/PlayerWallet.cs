using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private float money = 100f;

    public bool CanAfford(float amount) => money >= amount;
    public void SpendMoney(float amount) => money -= amount;
    public float GetMoney() => money;
}