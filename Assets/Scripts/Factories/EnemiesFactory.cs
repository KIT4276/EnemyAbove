using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesFactory 
{
    [Inject]
    private Enemy.Pool _enemiesPool;
    [Inject]
    private Player _player;
    private readonly List<Enemy> _enemies = new List<Enemy>();

    public event SimpleHandle ChangeEnemiesCount;
    public event SimpleHandle DeadSumEnemyEvent;


    public Enemy SpawnEnemie(Transform transform)
    {

        var enemy = _enemiesPool.Spawn();
        enemy.SetPimary(true);
        _enemies.Add(enemy);
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;

        enemy.Restart();
        enemy.NavMeshEnabled(true);
        enemy.Detection();
        ChangeEnemiesCount?.Invoke();
        enemy.DeadEnemyEvent += DeadEnemy;
        return enemy;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemiesPool.Despawn(enemy);
        _enemies.Remove(enemy);
        enemy.NavMeshEnabled(false);
        ChangeEnemiesCount?.Invoke();
    }

    public int GetEnemiesCount()
        => _enemies.Count;

    private void DeadEnemy()
        => DeadSumEnemyEvent?.Invoke();
}
