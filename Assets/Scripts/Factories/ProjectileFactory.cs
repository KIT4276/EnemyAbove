using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileFactory 
{
    private readonly List<Projectile> _projectiles = new List<Projectile>();

    [Inject]
    private Projectile.Pool _projectilesPool;

    public Projectile SpawnProjectile(Transform transform)
    {
        var projectile = _projectilesPool.Spawn();
        _projectiles.Add(projectile);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        return projectile;
    }

    public void RemoveProjectile(Projectile projectile)
    {
        _projectilesPool.Despawn(projectile);
        _projectiles.Remove(projectile);
    }
}
