using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    private Enemy _holdableEnemy;
    public bool IsEmpty => _holdableEnemy == null;

    public void Fill(Enemy enemy) => _holdableEnemy = enemy;
    public void Clear(Enemy enemy)
    {
        if (_holdableEnemy == enemy)
            _holdableEnemy = null;
    }
}
