using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    private EnemyDeathService _enemyDeathService;
    private Enemy _enemyPrefab;

    [SerializeField] private List<SpawnPosition> _positions;

    private Dictionary<Enemy, SpawnPosition> _enemyPositions = new Dictionary<Enemy, SpawnPosition>();

    [SerializeField] private Sprite _logic;
    [SerializeField] private Sprite _time;
    [SerializeField] private Sprite _count;

    private void Update() => GetInput();

    public void Initialize(EnemyDeathService enemyDeathService, Enemy enemyPrefab)
    {
        _enemyDeathService = enemyDeathService;
        _enemyPrefab = enemyPrefab;

        _enemyDeathService.OnEnemyDestroy += ClearPositionForEnemy;
    }

    public void ClearPositionForEnemy(Enemy enemy)
    {
        if (_enemyPositions.TryGetValue(enemy, out SpawnPosition position))
        {
            position.Clear(enemy);
            _enemyPositions.Remove(enemy);
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SpawnEnemyWithCondition(_logic, enemy => enemy.IsDead);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SpawnEnemyWithCondition(_time, enemy => enemy.IsExpired);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SpawnEnemyWithCondition(_count, enemy => _enemyDeathService.RegisteredCount > 5);
    }

    private void SpawnEnemyWithCondition(Sprite sprite, Func<Enemy, bool> condition)
    {
        SpawnPosition emptyPosition = GetEmptyPosition();

        if (emptyPosition == null)
            return;

        Enemy spawnedEnemy = Instantiate(_enemyPrefab, emptyPosition.transform);
        Image enemyImage = spawnedEnemy.GetComponent<Image>();

        if (enemyImage != null)
            enemyImage.sprite = sprite;

        emptyPosition.Fill(spawnedEnemy);

        _enemyPositions[spawnedEnemy] = emptyPosition;

        _enemyDeathService.RegisterEnemy(
            spawnedEnemy,
            emptyPosition.transform,
            condition);
    }

    private SpawnPosition GetEmptyPosition()
    {
        for (int i = 0; i < _positions.Count; i++)
            if (_positions[i].IsEmpty)
                return _positions[i];

        return null;
    }

    private void OnDestroy()
    {
        if (_enemyDeathService != null)
            _enemyDeathService.OnEnemyDestroy -= ClearPositionForEnemy;
    }
}