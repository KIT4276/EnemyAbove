using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private GameObject _cameraPrefab;
    [SerializeField]
    private GameObject _faderPrefab;
    [SerializeField]
    private GameObject _loaderPrefab;
    [SerializeField]
    private GameObject _artefactsSystemPrefab;
    [SerializeField]
    private GameObject _dropLootSystemPrefab;
    [SerializeField]
    private GameObject _experienceSystemPrefab;
    [SerializeField]
    private GameObject _upgradeSystemPrefab;
    [SerializeField]
    private GameObject _upgradeMenuPrefab;
    [SerializeField]
    private GameObject _saveSystemPrefab;

    [Space, SerializeField]
    private Projectile _projectilePrefab;
    [SerializeField]
    private Enemy _enemyPrefab;
    [SerializeField]
    private Loot _lootPrefab;

    public override void InstallBindings()
    {
        InstallProjectileFactory();

        InstallPlayer();
        InstallCamera();

        InstallEnemiesWanderFactory();

        InstallFader();
        InstallLVLLoader();
        InstallLootFactory();
        InstallArtefactsSystem();
        InstallDropLootSystem();
        InstallExperienceSystem();
        InstallUpgradeSystem();
        InstallSaveSystem();
        InstallUpgradeMenu();
    }

    private void InstallProjectileFactory()
    {
        Container.Bind<ProjectileFactory>().AsSingle();
        Container.BindMemoryPool<Projectile, Projectile.Pool>().FromComponentInNewPrefab(_projectilePrefab);
    }

    private void InstallPlayer()
    {
        Player playerInstance = Container.InstantiatePrefabForComponent<Player>
            (_playerPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<Player>().FromInstance(playerInstance).AsSingle().NonLazy();
    }

    private void InstallCamera()
    {
        CameraPoint cameraInstance = Container.InstantiatePrefabForComponent<CameraPoint>
            (_cameraPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<CameraPoint>().FromInstance(cameraInstance).AsSingle().NonLazy();
    }

    private void InstallFader()
    {
        Fader faderInstance = Container.InstantiatePrefabForComponent<Fader>
                    (_faderPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<Fader>().FromInstance(faderInstance).AsSingle().NonLazy();
    }

    private void InstallLVLLoader()
    {
        LVLLoader loaderInstance = Container.InstantiatePrefabForComponent<LVLLoader>
                (_loaderPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<LVLLoader>().FromInstance(loaderInstance).AsSingle().NonLazy();
    }

    private void InstallArtefactsSystem()
    {
        ArtefactsSystem artefactsSystem = Container.InstantiatePrefabForComponent<ArtefactsSystem>
            (_artefactsSystemPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<ArtefactsSystem>().FromInstance(artefactsSystem).AsSingle().NonLazy();
    }

    private void InstallEnemiesWanderFactory()
    {
        Container.Bind<EnemiesFactory>().AsSingle();
        Container.BindMemoryPool<EnemyWander, EnemyWander.Pool>().FromComponentInNewPrefab(_enemyPrefab);
    }

    private void InstallLootFactory()
    {
        Container.Bind<LootFactory>().AsSingle();
        Container.BindMemoryPool<Loot, Loot.Pool>().FromComponentInNewPrefab(_lootPrefab);
    }

    private void InstallDropLootSystem()
    {
        DropLootSystem ddopLootSystem = Container.InstantiatePrefabForComponent<DropLootSystem>
            (_dropLootSystemPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<DropLootSystem>().FromInstance(ddopLootSystem).AsSingle().NonLazy();
    }

    private void InstallExperienceSystem()
    {
        ExperienceSystem eperienceSystem = Container.InstantiatePrefabForComponent<ExperienceSystem>
            (_experienceSystemPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<ExperienceSystem>().FromInstance(eperienceSystem).AsSingle().NonLazy();
    }

    private void InstallUpgradeSystem()
    {
        UpgradeSystem upgradeSystem = Container.InstantiatePrefabForComponent<UpgradeSystem>
            (_upgradeSystemPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<UpgradeSystem>().FromInstance(upgradeSystem).AsSingle().NonLazy();
    }

    private void InstallUpgradeMenu()
    {
        UpgradeMenu upgradeMenu = Container.InstantiatePrefabForComponent<UpgradeMenu>
            (_upgradeMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<UpgradeMenu>().FromInstance(upgradeMenu).AsSingle().NonLazy();
    }

    private void InstallSaveSystem()
    {
        SaveSystem saveSystem = Container.InstantiatePrefabForComponent<SaveSystem>
            (_saveSystemPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        Container.Bind<SaveSystem>().FromInstance(saveSystem).AsSingle().NonLazy();
    }
}