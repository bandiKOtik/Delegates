using System;
using UnityEngine;
using UnityEngine.UI;

public class WalletInteractor : MonoBehaviour
{
    [SerializeField] private InputField _userInput;
    [SerializeField] private Dropdown _currencyDropdown;

    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;

        _currencyDropdown.ClearOptions();

        string[] enumNames = Enum.GetNames(typeof(Currencies));

        var options = new System.Collections.Generic.List<Dropdown.OptionData>();

        foreach (var enumName in enumNames)
            options.Add(new Dropdown.OptionData(enumName));

        _currencyDropdown.AddOptions(options);
    }

    public void AppendValue()
    {
        if (TryGetCurrency(out var currency))
            _wallet.Append(currency, ParsedUsedInput(_userInput.text));
        else
            Debug.LogError("Wallet does not support this type of currency.");
    }

    public void SubtractValue()
    {
        if (TryGetCurrency(out var currency))
            _wallet.Subtract(currency, ParsedUsedInput(_userInput.text));
        else
            Debug.LogError("Wallet does not support this type of currency.");
    }

    private bool TryGetCurrency(out Currencies currencies)
    {
        Currencies selectedCurrency = (Currencies)_currencyDropdown.value;

        if (_wallet?.ContainsKey(selectedCurrency) != null)
        {
            currencies = (Currencies)_currencyDropdown.value;
            return true;
        }
        else
        {
            currencies = default;
            return false;
        }
    }

    private int ParsedUsedInput(string value)
    {
        if (int.TryParse(_userInput.text, out int parsed))
        {
            return parsed;
        }
        else
        {
            Debug.LogError("Can't parse value.");
            return default;
        }
    }
}
