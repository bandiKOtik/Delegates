using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wallet
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _currencyParentTransform;
        [SerializeField] private CurrenciesSettingsUI _settingsUI;

        private Wallet _wallet;

        private Dictionary<Currencies, CounterView> _currenciesCounterViews = new();

        public void Initialize(Wallet wallet)
        {
            _wallet = wallet;

            SpawnTextFields(_wallet);
        }

        private void SpawnTextFields(Wallet wallet)
        {
            foreach (var key in wallet.Stash.Keys)
            {
                var counter = Instantiate(_settingsUI.GetVariantByType(key), _currencyParentTransform);

                counter.Initialize(wallet.GetCurrencyVariable(key));

                _currenciesCounterViews.Add(key, counter);
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
            return _currenciesVariants.Find(x => x.Currency == currencies).Variant;
        }
    }
}