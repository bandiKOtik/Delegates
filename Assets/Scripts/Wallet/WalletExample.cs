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
            List<Currencies> currenciesList = new List<Currencies>()
        {
            Currencies.Coins, Currencies.Gems, Currencies.Energy
        };

            _wallet = new Wallet(currenciesList);

            _view = Instantiate(_view);

            _interactor = _view.GetComponentInChildren<WalletInteractor>();

            _interactor.Initialize(_wallet);
            _view.Initialize(_wallet);
        }
    }
}