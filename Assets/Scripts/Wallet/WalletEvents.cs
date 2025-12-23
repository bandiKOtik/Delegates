using System;

public class WalletEvents
{
    public event Action<Currencies, int> Increase;
    public event Action<Currencies, int> Decrease;

}
