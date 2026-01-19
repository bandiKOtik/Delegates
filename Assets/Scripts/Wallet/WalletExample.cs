using System.Collections.Generic;
using UnityEngine;

namespace Wallet
{
    public class WalletExample : MonoBehaviour
    {
        [SerializeField] private WalletUI _view;
        private WalletInteractor _interactor;
        private Wallet _wallet;

        private void Start()
        {
            List<(Currencies, int)> currenciesList = new()
        {
            (Currencies.Coins, 50),
            (Currencies.Gems, 0),
            (Currencies.Energy, 100)
        };

            _wallet = new Wallet(currenciesList);

            _view = Instantiate(_view);

            _interactor = _view.GetComponentInChildren<WalletInteractor>();

            _view.Initialize(_wallet);
            _interactor.Initialize(_wallet);
        }
    }
}