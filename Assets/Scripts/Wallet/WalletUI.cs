using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletUI : MonoBehaviour
{
    private Wallet _wallet;

    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _gemsText;
    [SerializeField] private Text _energyText;

    private Dictionary<Currencies, Text> _currencyTextFields;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;

        _currencyTextFields = new Dictionary<Currencies, Text>()
        {
            { Currencies.Coins, _coinsText },
            { Currencies.Gems, _gemsText },
            { Currencies.Energy, _energyText }
        };

        _wallet.OnCurrencieChanged += UpdateValue;
    }

    private void OnDisable() => _wallet.OnCurrencieChanged -= UpdateValue;

    private void UpdateValue(Currencies currencie, int amount)
        => _currencyTextFields[currencie].text = amount.ToString();
}