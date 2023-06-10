using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DropLootSystem : MonoBehaviour
{
    private List<Loot> _loots = new List<Loot>();

    [SerializeField]
    private float _dropTime = 2;
    [SerializeField]
    private float _jumpPower = 2;
    [SerializeField]
    private int _numJumps = 1;

    [Inject]
    private LootFactory _lootFactory;
    [Inject]
    private LVLLoader _lVLLoader;

    private void Start()
       => _lVLLoader.LoadSceneCompliteEvent += Despawn;

    private void Despawn(string name)
    {
        foreach (var loot in _loots)
        {
            if (!loot.IsInPool)
                _lootFactory.RemoveLoot(loot);
        }
    }

    private void TweenLoot(Transform lootTransform, Transform pointTransform)
       => lootTransform.DOJump(pointTransform.position, _jumpPower, _numJumps, _dropTime);

    public void DropLoot(Transform transform)
    {
        LootType lootType = (LootType)Random.Range(1, 4);
        Loot loot = _lootFactory.SpawnLoot(transform);
        _loots.Add(loot);

        switch (lootType)
        {
            case LootType.TearAmmo:
                loot.SetLootType(LootType.TearAmmo);
                break;
            case LootType.FurtherAmmo:
                loot.SetLootType(LootType.FurtherAmmo);
                break;
            case LootType.AidKit:
                loot.SetLootType(LootType.AidKit);
                break;
        }
        TweenLoot(loot.transform, transform);
    }
}
