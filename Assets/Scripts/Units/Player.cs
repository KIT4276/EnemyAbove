using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : BaseUnit
{
    private PlayerControls _controls;

    private int _layerMask;
    private RaycastHit _hit;
    private Ray _ray;
    private Vector3 _movement;
    private Transform _playersTransform;
    private PlayerSounds _sounds;

    private PlayersWeapon _playersWeapon;
    [SerializeField]
    private float _delayAfterDeath = 3;
    [SerializeField]
    private float _moveSpeed = 1f;

    public event SimpleHandle PlayerChangeHealthEvent;
    public event SimpleHandle PlayerAmmoChangeEvent;
    public event SimpleHandle PlayersDeathEvent;
    public event SimpleHandle GetArtifactEvent;
    public event SimpleHandle PlayerFireEvent;

    private void Start()
    {
        _playersTransform = GetComponent<Transform>();
        _sounds = GetComponent<PlayerSounds>();
        _controls = new PlayerControls();
        _controls.PlayerActionMap.Enable();
        _playersWeapon = GetComponent<PlayersWeapon>();
        _controls.PlayerActionMap.Attack.performed += Fire;
        _controls.PlayerActionMap.WeaponSwitch.performed += WeaponSwitch;
        _sideType = SideType.Player;
        _isShotPossible = false;

        var name = SceneManager.GetActiveScene().name;
        if (name != "LVL0" && name != "BetweenLVL") ShootingPermits();
    }


    private void FixedUpdate()
    {
        _directionToWalk = _controls.PlayerActionMap.Move.ReadValue<Vector2>();
        _movement = new Vector3(_directionToWalk.x, 0f, _directionToWalk.y).normalized;

        if (IsAlive)
        {
            OnMove();
            OnRotate();
        }
    }

    private void WeaponSwitch(InputAction.CallbackContext obj)
    {
        _sounds.PlayWeaponSwitch();
        _playersWeapon.WeaponSwitch();
        _isShotPossible = _weaponClass.GetAbilityToShoot();
    }

    public override void SetDamage(float damage)
    {
        base.SetDamage(damage);
        PlayerChangeHealthEvent?.Invoke();
    }

    private void OnMove()
    {
        transform.position += _movement * _moveSpeed * Time.deltaTime;

        if (_movement == new Vector3(0f, 0f, 0f))
        {
            _actionType = ActionType.Idle;
        }
        else
            _actionType = ActionType.Idle;
    }

    private void OnRotate()
    {
        _layerMask = 1 << 6;
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 1000, _layerMask))
        {
            _targetForLookAt = new Vector3(_hit.point.x, transform.position.y, _hit.point.z);

            _inputVector = (Vector3.forward * _directionToWalk.y) + (Vector3.right * _directionToWalk.x);

            _animationVector = _playersTransform.transform.InverseTransformDirection(_inputVector);

            _animator.SetFloat(SideMove, _animationVector.x);
            _animator.SetFloat(ForvardMove, _animationVector.z);
        }

        _playersTransform.LookAt(_targetForLookAt);
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        if (Fader.IsFading || !IsAlive || !_isShotPossible) return;

        if (_canShot)
        {
            StartCoroutine(HoldShot());

            _playersWeapon.AmmoStatusUpdate();
            PlayerAmmoChangeEvent?.Invoke();
            PlayerFireEvent?.Invoke();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseTrigger>() == null) return;
        base.OnTriggerEnter(other);

        if (_triggerType == TriggerType.Loot)
        {
            _sounds.PlayTakeLoot();

            var lootType = other.GetComponent<Loot>().LootType;

            switch (lootType)
            {
                case LootType.TearAmmo:
                    _weaponClass.AddAmmo(other.GetComponent<BaseTrigger>().GetValue(), ProjectileType.Tear);

                    break;
                case LootType.FurtherAmmo:
                    _weaponClass.AddAmmo(other.GetComponent<BaseTrigger>().GetValue(), ProjectileType.Further);

                    break;
                case LootType.AidKit:
                    _currentHealth += other.GetComponent<BaseTrigger>().GetValue();
                    if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
                    PlayerChangeHealthEvent?.Invoke();
                    break;
                default:
                    break;
            }
            other.GetComponent<Loot>().ReturnToPool();
            PlayerAmmoChangeEvent?.Invoke();
        }

        if (_triggerType == TriggerType.Artifact)
        {
            _sounds.PlayTakeArtifact();

            GetArtifactEvent?.Invoke();
            _actionType = ActionType.Idle;
            other.gameObject.SetActive(false);
        }
    }

    protected override void Death()
    {
        if (!IsAlive) return;
        base.Death();
        PlayersDeathEvent?.Invoke();
    }

    public void Restart()
    {
        transform.position = Vector3.zero;
        IsAlive = true;
        _currentHealth = _maxHealth;
        _weaponClass.Restart();
        _animator.SetBool(Die, false);
    }

    public void ShootingPermits()
        => _isShotPossible = true;

    public void MovingingPermits()
        => _controls.PlayerActionMap.Enable();

    public void ShootingBan()
        => _isShotPossible = false;

    public void MovingingBan()
        => _controls.PlayerActionMap.Disable();

    public void StopAnimation()
        => _animator.enabled = false;

    #region GetMethods

    public float GetSpeed()
        => _moveSpeed;

    public float GetDelayAfterDeath()
        => _delayAfterDeath;

    public float GetCoolDown()
        => _coolDown;

    public PlayerControls GetControls()
        => _controls;

    public PlayersWeapon GetPlayersWeapon()
        => _playersWeapon;

    public Vector3 GetPlayersPos()
    {
        if (this == null)
            return Vector3.zero;
        else
            return this.transform.position;
    }

    #endregion GetMethods

    #region SetMethods


    public void DecreaseÑoolDown(float value)
        => _coolDown -= value;

    public void IncreaseMaxHP(float value)
        => _maxHealth += value;

    public void IncreaseMoveSpeed(float value)
        => _moveSpeed += value;

    public void SetMaxHP(float value)
        => _maxHealth = value;

    public void SetÑoolDown(float value)
        => _coolDown = value;

    public void SetMoveSpeed(float value)
        => _moveSpeed = value;

    #endregion SetMethods

    public override void Step()
        => _sounds.Step();

    private void OnDestroy()
    {
        _controls.PlayerActionMap.Attack.performed -= Fire;
        _controls.PlayerActionMap.WeaponSwitch.performed -= WeaponSwitch;
        _controls.Dispose();
    }

    private void OnDisable()
        => _controls.PlayerActionMap.Disable();
}
