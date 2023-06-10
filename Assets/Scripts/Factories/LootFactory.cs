using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LootFactory 
{
    private readonly List<Loot> _loots = new List<Loot>();

    [Inject]
    private Loot.Pool _lootPool;

    public Loot SpawnLoot(Transform transform)
    {
        var loot = _lootPool.Spawn();
        _loots.Add(loot);
        loot.transform.position = transform.position;
        loot.transform.rotation = transform.rotation;
        loot.Restart();
        return loot;
    }

    public void RemoveLoot(Loot loot)
    {
        _lootPool.Despawn(loot);
        _loots.Remove(loot);
        loot.Despawn();
    }
}
