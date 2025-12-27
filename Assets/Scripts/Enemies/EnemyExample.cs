using UnityEngine;

public class EnemyExample : MonoBehaviour
{
    private EnemyDeathService _enemyDeathService;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Enemy _enemyPrefab;

    private void Start()
    {
        _enemyDeathService = new EnemyDeathService();

        _spawner.Initialize(_enemyDeathService, _enemyPrefab);

        StartCoroutine(_enemyDeathService.CheckConditions());
    }
}
