using UnityEngine;
using Zenject;

public class EnemiesSpawnerInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _enemiesSpawner;

    public override void InstallBindings()
    {
        Container.Bind<EnemiesSpawner>().FromComponentOn(_enemiesSpawner).AsSingle().NonLazy();
    }
}