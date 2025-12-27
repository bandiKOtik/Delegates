using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathService
{
    public event Action<Enemy> OnEnemyDestroy;

    private Dictionary<Enemy, Func<Enemy, bool>> _registeredEnemies = new Dictionary<Enemy, Func<Enemy, bool>>();

    public int RegisteredCount => _registeredEnemies.Count;

    public void RegisterEnemy(Enemy enemy, Transform parent, Func<Enemy, bool> condition)
    {
        if (parent == null)
            return;

        _registeredEnemies.Add(enemy, condition);
    }

    public IEnumerator CheckConditions()
    {
        while (true)
        {
            var enemiesToCheck = new List<Enemy>(_registeredEnemies.Keys);

            foreach (Enemy enemy in enemiesToCheck)
            {
                if (_registeredEnemies.TryGetValue(enemy, out Func<Enemy, bool> condition))
                {
                    bool shouldDie = condition(enemy);

                    if (shouldDie)
                        DestroyEnemmy(enemy);
                }
            }

            Debug.Log($"Total enemy count: {_registeredEnemies.Count}");
            yield return null;
        }
    }

    private void DestroyEnemmy(Enemy enemy)
    {
        GameObject.Destroy(enemy.gameObject);
        _registeredEnemies.Remove(enemy);
        OnEnemyDestroy?.Invoke(enemy);
    }
}