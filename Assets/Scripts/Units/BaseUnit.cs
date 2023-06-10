using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseUnit : MonoBehaviour
{
    protected float _currentHealth;

    protected bool _isShotPossible = true;
    protected bool _canShot = true;
    protected ActionType _actionType;
    protected Vector3 _inputVector;
    protected Vector3 _animationVector;
    protected Vector2 _directionToWalk;
    protected Vector3 _targetForLookAt;
    protected SideType _sideType;
    protected TriggerType _triggerType;
    protected Animator _animator;
    protected BaseWeapon _weaponClass;
    protected UnitSounds _unitSounds;

    [SerializeField]
    protected float _maxHealth = 20f;
    [SerializeField]
    protected Transform _weaponTransform;
    [SerializeField]
    protected float _coolDown = 1;

    [Inject]
    protected ProjectileFactory _projectileFactory;

    protected readonly int ForvardMove = Animator.StringToHash("ForvardMove");
    protected readonly int SideMove = Animator.StringToHash("SideMove");
    protected readonly int Die = Animator.StringToHash("Die");
    protected readonly int Move = Animator.StringToHash("Move");

    public bool IsAlive { get; protected set; }

    protected void Awake()
    {
        IsAlive = true;
        _unitSounds = GetComponent<UnitSounds>();
        _animator = GetComponent<Animator>();
        _weaponClass = GetComponent<BaseWeapon>();
        _currentHealth = _maxHealth;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseTrigger>() == null) return;

        _triggerType = other.GetComponent<BaseTrigger>().GetTriggerType();

        if (_triggerType == TriggerType.Projectile)
        {
            if (other.GetComponent<Projectile>().SideType != _sideType)
            {
                SetDamage(other.GetComponent<Projectile>().Damage);
                other.gameObject.SetActive(false);
            }
        }
    }

    protected void ToShoot(Transform weapon)
    {
        _isShotPossible = _weaponClass.GetAbilityToShoot();

        Projectile projectile;

        if (_isShotPossible)
        {
            _unitSounds.PlayShot();

            projectile = _projectileFactory.SpawnProjectile(_weaponTransform);

            switch (_weaponClass.ActiveWeapon)
            {
                case ProjectileType.Tear:
                    projectile.SetProjectileType(ProjectileType.Tear);
                    break;
                case ProjectileType.Further:
                    projectile.SetProjectileType(ProjectileType.Further);
                    break;
            }

            projectile.SideType = _sideType;
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log(" -------------- Нет патронов для стрельбы---------------");
        }
#endif
    }

    protected IEnumerator HoldShot()
    {
        _canShot = false;
        Attack();

        yield return new WaitForSeconds(_coolDown);
        _canShot = true;
    }

    protected void Attack()
    {
        if (IsAlive)
            ToShoot(_weaponTransform);
    }

    protected virtual void Death()
    {
        if (!IsAlive) return;
        _unitSounds.PlayDeath();
        IsAlive = false;
        _animator.SetTrigger(Die);
    }

    public virtual void SetDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Death();
    }

    public void RestoreHealth(float restore)
    {
        _currentHealth += restore;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
    }

    public float GetMaxHealth() => _maxHealth;

    public float GetCurrentHealth() => _currentHealth;

    public virtual void Step() { }//Stub
}
