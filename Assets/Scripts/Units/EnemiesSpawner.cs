using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesSpawner : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    [SerializeField]
    private List<Transform> _spawnPoints;

    [Inject]
    private EnemiesFactory _enemiesFactory;
    [Inject]
    private LVLLoader _lVLLoader;

    private void Start()
    {
        SpawnSceneEnemies();
        _lVLLoader.LoadSceneEvent += DespawnAll;
    }

    private void SpawnSceneEnemies()
    {
        foreach (var point in _spawnPoints)
        {
            var enemy = _enemiesFactory.SpawnEnemie(point);
            EnterParams(enemy, point);
        }
    }

    private void DespawnAll()
    {
        if (_lVLLoader.GetCurrentSceneName() == "LVL0") return;

        foreach (var enemy in _enemies)
        {
            if (!enemy.IsInPool)
            {
                enemy.Despawn();
            }
        }
    }

    public void EnterParams(Enemy enemy, Transform point)
    {
        _enemies.Add(enemy);
        enemy.Restart();
        enemy.SetCentralPoint(point.position);
    }

    private void OnDisable()
        => _lVLLoader.LoadSceneEvent -= DespawnAll;
}
