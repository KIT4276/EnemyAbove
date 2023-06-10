public class PlayersWeapon : BaseWeapon
{
    public event SimpleHandle WeaponSwitchEvent;

    private void Awake()
    {
        _ammoInMagTear = _magazineCapacity;
        _ammoInMagFurther = _magazineCapacity;
        ActiveWeapon = ProjectileType.Tear;
    }

    public void AmmoStatusUpdate()
    {
        switch (ActiveWeapon)
        {
            case ProjectileType.Tear:
                _ammoInMagTear--;
                if (_ammoInMagTear <= 0) Reload();
                break;
            case ProjectileType.Further:
                _ammoInMagFurther--;
                if (_ammoInMagFurther <= 0) Reload();
                break;
        }
    }

    public void WeaponSwitch()
    {
        switch (ActiveWeapon)
        {
            case ProjectileType.Tear:
                ActiveWeapon = ProjectileType.Further;
                _tearWeaponObject.SetActive(false);
                _furtherWeaponObject.SetActive(true);
                break;
            case ProjectileType.Further:
                ActiveWeapon = ProjectileType.Tear;
                _tearWeaponObject.SetActive(true);
                _furtherWeaponObject.SetActive(false);
                break;
        }

        WeaponSwitchEvent?.Invoke();
    }

    public override bool GetAbilityToShoot()
    {
        if (ActiveWeapon == ProjectileType.Further)
        {
            if (_leftAmmoFurther != 0 || _ammoInMagFurther != 0) return true;
            else return false;
        }
        else
        {
            if (_leftAmmoTear != 0 || _ammoInMagTear != 0) return true;
            else return false;
        }
    }

    public override void Restart()
    {
        _leftAmmoTear = _magazineCapacity * 3;
        _leftAmmoFurther = _magazineCapacity * 3;
        _ammoInMagTear = _magazineCapacity;
        _ammoInMagFurther = _magazineCapacity;

        ActiveWeapon = ProjectileType.Tear;

        base.Restart();
    }

    #region GetMethods
    public float GetMagazineCapacity()
        => _magazineCapacity;

    public float GetLeftAmmoTear()
        => _leftAmmoTear;

    public float GetLeftAmmoFurther()
        => _leftAmmoFurther;

    public float GetAmmoInMagTear()
        => _ammoInMagTear;

    public float GetAmmoInMagFurther()
        => _ammoInMagFurther;

    #endregion GetMethods

    #region SetMethods

    public void IncreaseStockAmmo(float value)
        => _leftAmmoStart += value;

    public void SetStockAmmo(float value)
        => _leftAmmoStart = value;

    #endregion SetMethods
}
