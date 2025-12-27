using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool _isDead = false;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private int _maxCount = 5;

    public bool IsDead => _isDead;
    public float SpawnTime { get; private set; } = 0;
    public float TimeSinceSpawn => Time.time - SpawnTime;
    public bool IsExpired => TimeSinceSpawn > _lifeTime;


    public void Initialize()
    {
        StartCoroutine(ProcessLifetime());
    }

    private IEnumerator ProcessLifetime()
    {
        while (true)
            SpawnTime += Time.time;
    }

    public void SetToDead() => _isDead = true;
}
