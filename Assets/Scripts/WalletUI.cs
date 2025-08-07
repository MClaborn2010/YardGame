using UnityEngine;
using TMPro;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private PlayerWallet wallet;
    [SerializeField] private TextMeshProUGUI walletText;

    private void Start()
    {
        UpdateWalletDisplay();
    }

    public void UpdateWalletDisplay()
    {
        if (wallet != null && walletText != null)
        {
            float money = wallet.GetMoney();
            string moneyText = $"Money: ${money:F2}";
            walletText.text = moneyText;
            walletText.ForceMeshUpdate(true);
        }
        else
        {
            Debug.LogError("WalletUI: Cannot update display, wallet or walletText is null!");
        }
    }
}