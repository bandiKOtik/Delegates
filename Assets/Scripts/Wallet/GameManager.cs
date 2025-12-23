using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int _levelMinUpgradeCost = 2;
    private const int _levelUpgradeCostMultiplier = 2;

    public int CurrentLevel {  get; private set; }
}
