using System;
using System.Collections.Generic;

public enum Currencies
{
    Coins = 0,
    Gems = 1,
    Energy = 2
}

public class Wallet
{
    public event Action<Currencies, int> OnCurrencieChanged;

    public readonly Dictionary<Currencies, int> CurrencyStash;

    public Wallet()
    {
        CurrencyStash = new Dictionary<Currencies, int>
        {
            { Currencies.Coins, default},
            { Currencies.Gems, default},
            { Currencies.Energy, default}
        };
    }

    public void ChangeCurrencieValue(Currencies currencie, int amount)
    {
        CurrencyStash[currencie] += amount;

        OnCurrencieChanged?.Invoke(currencie, CurrencyStash[currencie]);
    }
}
