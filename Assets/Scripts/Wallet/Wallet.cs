using System;
using System.Collections.Generic;

namespace Wallet
{
    public class Wallet
    {
        private Dictionary<Currencies, ReactiveVariable<int>> CurrencyStash = new();
        public IReadOnlyDictionary<Currencies, ReactiveVariable<int>> Stash => CurrencyStash;

        public Wallet(List<Currencies> currencies)
        {
            foreach (Currencies currency in currencies)
                CurrencyStash.Add(currency, new ReactiveVariable<int>());
        }

        public Wallet(List<(Currencies, int)> currencies)
        {
            foreach ((Currencies, int) currency in currencies)
                CurrencyStash.Add(currency.Item1, new ReactiveVariable<int>(currency.Item2));
        }

        public void Append(Currencies currency, int amount)
        {
            if (CurrencyStash.ContainsKey(currency) == false)
                throw new KeyNotFoundException($"Currency {currency} not found is Stash.");

            if (TryAppend(CurrencyStash[currency].Value, amount) == false)
                throw new InvalidOperationException($"Cannot append {amount} to currency {currency}.");

            CurrencyStash[currency].Value += amount;
        }

        public void Subtract(Currencies currency, int amount)
        {
            if (CurrencyStash.ContainsKey(currency) == false)
                throw new KeyNotFoundException($"Currency {currency} not found is Stash.");

            if (TrySubtract(CurrencyStash[currency].Value, amount, out var remainder) == false)
                throw new InvalidOperationException($"Cannot subtract {amount} to currency {currency}.");

            CurrencyStash[currency].Value = remainder;
        }

        private bool TryAppend(int value, int amount)
        {
            if (amount <= 0)
                return false;

            try
            {
                checked
                {
                    int result = value + amount;
                    return result >= 0;
                }
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        private bool TrySubtract(int value, int amount, out int remainder)
        {
            remainder = 0;

            if (amount <= 0)
                return false;

            try
            {
                checked
                {
                    remainder = value - amount;
                    return remainder >= 0;
                }
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        public IReadOnlyVariable<int> GetCurrencyVariable(Currencies currency)
        {
            if (CurrencyStash.TryGetValue(currency, out var variable))
                return (IReadOnlyVariable<int>)variable;

            throw new ArgumentNullException("No currencies.");
        }
    }
}