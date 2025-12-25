using System;
using UnityEngine;
using UnityEngine.UI;

public class WalletCanvasInteractor : MonoBehaviour
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

    public void AppendValue() => ModifyValue(SelectedCurrencie(), ParsedUsedInput(_userInput.text));
    public void SubtractValue() => ModifyValue(SelectedCurrencie(), -ParsedUsedInput(_userInput.text));

    private void ModifyValue(Currencies currencie, int amount) => _wallet.ChangeCurrencieValue(currencie, amount);

    private Currencies SelectedCurrencie() => (Currencies)_currencyDropdown.value;

    private int ParsedUsedInput(string value)
    {
        if (int.TryParse(_userInput.text, out int parsed))
        {
            return parsed;
        }
        else
        {
            Debug.LogError("Can't parse value.");
            return 0;
        }
    }
}
