using System;
using System.Collections;
using System.Collections.Generic;

public class Wallet : IReadOnlyDictionary<Currencies, int>
{
    public event Action<Currencies, int> CurrencyChanged;

    private Dictionary<Currencies, int> CurrencyStash = new Dictionary<Currencies, int>();

    public IEnumerable<Currencies> Keys => ((IReadOnlyDictionary<Currencies, int>)CurrencyStash).Keys;

    public IEnumerable<int> Values => ((IReadOnlyDictionary<Currencies, int>)CurrencyStash).Values;

    public int Count => ((IReadOnlyCollection<KeyValuePair<Currencies, int>>)CurrencyStash).Count;

    public int this[Currencies key] => ((IReadOnlyDictionary<Currencies, int>)CurrencyStash)[key];


    public Wallet(List<Currencies> currencies)
    {
        foreach (Currencies currency in currencies)
            CurrencyStash.Add(currency, default);
    }

    public void Append(Currencies currencie, int amount)
    {
        if (amount == 0)
            return;

        CurrencyStash[currencie] += amount;

        CurrencyChanged?.Invoke(currencie, CurrencyStash[currencie]);
    }

    public void Subtract(Currencies currencie, int amount)
    {
        if (CurrencyStash[currencie] == 0 || amount == 0)
            return;

        int remainder = CurrencyStash[currencie] - amount;

        if (remainder >= 0)
            CurrencyStash[currencie] = remainder;
        else
            CurrencyStash[currencie] = 0;

        CurrencyChanged?.Invoke(currencie, CurrencyStash[currencie]);
    }

    public bool ContainsKey(Currencies key)
    {
        return ((IReadOnlyDictionary<Currencies, int>)CurrencyStash).ContainsKey(key);
    }

    public bool TryGetValue(Currencies key, out int value)
    {
        return ((IReadOnlyDictionary<Currencies, int>)CurrencyStash).TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<Currencies, int>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<Currencies, int>>)CurrencyStash).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)CurrencyStash).GetEnumerator();
    }
}
