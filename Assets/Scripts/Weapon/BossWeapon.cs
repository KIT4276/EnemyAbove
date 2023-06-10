using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : BaseWeapon
{
    private void Awake()
    {
        ActiveWeapon = ProjectileType.Non;
    }
}
