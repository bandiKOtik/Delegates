using UnityEngine;
using UnityEngine.UI;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _gemsText;
    [SerializeField] private Text _energyText;

    private void Start()
    {

    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _coinsText.text = _wallet.Coins.ToString();
        _gemsText.text = _wallet.Gems.ToString();
        _energyText.text = _wallet.Energy.ToString();
    }
}
