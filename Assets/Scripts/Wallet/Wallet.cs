using UnityEngine;

public enum Currencies
{
    coins = 0,
    gems = 1,
    energy = 2
}

public class Wallet : MonoBehaviour
{
    public int Coins { get; private set; }
    public int Gems { get; private set; }
    public int Energy { get; private set; }
}
