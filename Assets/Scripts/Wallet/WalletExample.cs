using System.Collections.Generic;
using UnityEngine;

public class WalletExample : MonoBehaviour
{
    [SerializeField] private WalletInteractor _interactor;
    [SerializeField] private WalletUI _view;

    private Wallet _wallet;

    private void Start()
    {
        List<Currencies> currenciesList = new List<Currencies>()
        {
            Currencies.Coins, Currencies.Gems, Currencies.Energy
        };

        _wallet = new Wallet(currenciesList);

        _interactor?.Initialize(_wallet);
        _view?.Initialize(_wallet);
    }
}
