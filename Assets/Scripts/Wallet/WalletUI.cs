using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wallet
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _currencyParentTransform;
        [SerializeField] private Dropdown _currencyDropdown;
        [SerializeField] private CurrenciesSettingsUI _settingsUI;

        private Wallet _wallet;

        private Dictionary<Currencies, CounterView> _currenciesCounterViews = new();

        public void Initialize(Wallet wallet)
        {
            _wallet = wallet;

            SpawnTextFields(_wallet);

            InitDropdown();
        }

        private void SpawnTextFields(Wallet wallet)
        {
            foreach (var key in wallet.Stash.Keys)
            {
                if (_settingsUI.GetVariantByType(key) == null)
                {
                    Debug.Log("UI settings not contains key: " +  key);
                    continue;
                }

                var counter = Instantiate(_settingsUI.GetVariantByType(key), _currencyParentTransform);

                counter.Initialize((ReactiveVariable<int>)wallet.GetCurrencyVariable(key));

                _currenciesCounterViews.Add(key, counter);
            }
        }

        private void InitDropdown()
        {
            if (_currenciesCounterViews.Count > 0)
            {
                _currencyDropdown.ClearOptions();

                List<string> enumNames = new();

                foreach (var name in _currenciesCounterViews.Keys)
                    enumNames.Add(name.ToString());

                var options = new List<Dropdown.OptionData>();

                foreach (var name in enumNames)
                    options.Add(new Dropdown.OptionData(name));

                _currencyDropdown.AddOptions(options);
            }
            else
            {
                Debug.LogError("Something happend with Currency Dropdown in UI.");
            }
        }
    }

    [Serializable]
    public class CurrenciesSettingsUI
    {
        [SerializeField] private List<CurrenciesConfigUI> _currenciesVariants = new();

        [Serializable]
        private class CurrenciesConfigUI
        {
            [field: SerializeField] public Currencies Currency { get; private set; }
            [field: SerializeField] public CounterView Variant { get; private set; }
        }

        public CounterView GetVariantByType(Currencies currencies)
        {
            var config = _currenciesVariants.Find(x => x.Currency == currencies);
            return config?.Variant;
        }
    }
}