using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    protected float _ammoInMagTear;
    protected float _ammoInMagFurther;
    protected float _leftAmmoTear;
    protected float _leftAmmoFurther;
    protected string _pathToPrefab;

    [SerializeField]
    protected float _magazineCapacity = 15;
    [SerializeField, Tooltip("How many ammo does a unit have at the start")]
    protected float _leftAmmoStart;

    [Space, SerializeField]
    protected GameObject _tearWeaponObject;
    [SerializeField]
    protected GameObject _furtherWeaponObject;

    public ProjectileType ActiveWeapon { get; protected set; }

    protected void Start()
        => Restart();

    protected void Reload()
    {
        if (ActiveWeapon == ProjectileType.Further)
        {
            if (_leftAmmoFurther < _magazineCapacity)
            {
                _ammoInMagFurther = _leftAmmoFurther;
                _leftAmmoFurther = 0;
            }
            else
            {
                _leftAmmoFurther -= _magazineCapacity;
                _ammoInMagFurther = _magazineCapacity;
            }
        } //-------------------------------------------------How to get rid of all these crazy repetitions?
        else
        {
            if (_leftAmmoTear < _magazineCapacity)
            {
                _ammoInMagTear = _leftAmmoTear;
                _leftAmmoTear = 0;
            }
            else
            {
                _leftAmmoTear -= _magazineCapacity;
                _ammoInMagTear = _magazineCapacity;
            }
        }
    }

    public void AddAmmo(float value, ProjectileType type)
    {
        if (type == ProjectileType.Further)
        {
            _leftAmmoFurther += value;
            if (_ammoInMagFurther <= 0) Reload();
        }
        else
        {
            _leftAmmoTear += value;
            if (_ammoInMagTear <= 0) Reload();
        }
    }

    public virtual void Restart()
    {
        switch (ActiveWeapon)
        {
            case ProjectileType.Tear:
                _tearWeaponObject.SetActive(true);
                _furtherWeaponObject.SetActive(false);
                break;
            case ProjectileType.Further:
                _tearWeaponObject.SetActive(false);
                _furtherWeaponObject.SetActive(true);
                break;
            default:
                _tearWeaponObject.SetActive(false);
                _furtherWeaponObject.SetActive(false);
                break;
        }
    }

    public virtual bool GetAbilityToShoot() => true;

    public float GetLeftAmmoStart()
        => _leftAmmoStart;
}
