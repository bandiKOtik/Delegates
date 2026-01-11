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

        public void Append(Currencies currencie, int amount)
        {
            if (TryAppend(CurrencyStash[currencie].Value, amount) == false)
                return;

            CurrencyStash[currencie].Value += amount;
        }

        public void Subtract(Currencies currencie, int amount)
        {
            if (TrySubtract(CurrencyStash[currencie].Value, amount, out var remainder) == false)
                return;

            CurrencyStash[currencie].Value = remainder;
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

        public ReactiveVariable<int> GetCurrencyVariable(Currencies currency)
        {
            if (CurrencyStash.TryGetValue(currency, out var variable))
                return variable;
            return null;
        }
    }
}