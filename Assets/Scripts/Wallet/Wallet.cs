using System;
using System.Collections.Generic;
using UnityEngine;

public enum Currencies
{
    Coins = 0,
    Gems = 1,
    Energy = 2
}

public class Wallet
{
    public event Action<Currencies, int> OnCurrencieChanged;

    public int Coins { get; private set; }
    public int Gems { get; private set; }
    public int Energy { get; private set; }

    public readonly Dictionary<Currencies, int> _currencyStash;

    public Wallet()
    {
        _currencyStash = new Dictionary<Currencies, int>
        {
            { Currencies.Coins, Coins},
            { Currencies.Gems, Gems},
            { Currencies.Energy, Energy}
        };
    }

    public void ChangeCurrencieValue(Currencies currencie, int amount)
    {
        _currencyStash[currencie] += amount;

        OnCurrencieChanged?.Invoke(currencie, _currencyStash[currencie]);
    }
}
