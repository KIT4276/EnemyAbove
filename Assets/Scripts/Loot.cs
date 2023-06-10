using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Loot : MonoBehaviour
{
    [SerializeField]
    private GameObject _tearAmmo;
    [SerializeField]
    private GameObject _furtherAmmo;
    [SerializeField]
    private GameObject _aidKit;

    [Inject]
    private LootFactory _lootFactory;

    public bool IsInPool { get; private set; }
    public LootType LootType { get; private set; }

    public void ReturnToPool()
    {
        _lootFactory.RemoveLoot(this);
        IsInPool = true;
    }

    public void Restart()
        => IsInPool = false;

    public void Despawn()
        => IsInPool = true;

    public void SetLootType(LootType type)
    {
        LootType = type;
        IsInPool = false;

        switch (type)
        {
            case LootType.TearAmmo:
                _tearAmmo.SetActive(true);
                _furtherAmmo.SetActive(false);
                _aidKit.SetActive(false);
                break;
            case LootType.FurtherAmmo:
                _tearAmmo.SetActive(false);
                _furtherAmmo.SetActive(true);
                _aidKit.SetActive(false);
                break;
            case LootType.AidKit:
                _tearAmmo.SetActive(false);
                _furtherAmmo.SetActive(false);
                _aidKit.SetActive(true);
                break;
        }
    }

    public class Pool : MonoMemoryPool<Loot>
    {
    }
}
