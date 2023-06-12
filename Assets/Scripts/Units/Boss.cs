using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Boss : BaseUnit
{
    private bool _isStarted;
    protected FieldOfView _fieldOfView;

    private readonly int CritAttack = Animator.StringToHash("Attack");

    [SerializeField]
    protected NavMeshAgent _navMeshAgent;

    [Space, SerializeField]
    private float _critDamage = 7;

    [Space, SerializeField]
    private BossTrigger _bossTrigger;
    [SerializeField]
    private float _deathDelay = 2.5f;
    [SerializeField]
    private float _spawnDelay = 5;
    [SerializeField]
    private float _recoveryDelay = 10;
    [SerializeField]
    private float _attackPause = 8;
    [SerializeField]
    private Transform[] _additSpawnPoints;
    [SerializeField]
    private GameObject _lastArtifact;

    [Space, SerializeField]
    private GameObject _healthBar;
    [SerializeField]
    private GameObject _text;
    [SerializeField]
    private GameObject _visCon;

    [Inject]
    private EnemiesSpawner _enemiesSpawner;
    [Inject]
    private CameraPoint _cameraPoint;
    [Inject]
    private EnemiesFactory _enemiesFactory;
    [Inject]
    private Player _player;

    private void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _sideType = SideType.Enemy;
        IsAlive = true;
        _currentHealth = _maxHealth;
        _canShot = true;
        _isShotPossible = true;
        _isStarted = false;
        _bossTrigger._bossTriggerFiredEvent += TurnOnBoss;
    }

    private void FixedUpdate()
    {
        if (_isStarted && IsAlive)
            _navMeshAgent.destination = _cameraPoint.transform.position;
        UpdateAnim();
    }

    private void UpdateAnim()
    {
        _animator.SetFloat(SideMove, _navMeshAgent.velocity.x);
        _animator.SetFloat(ForvardMove, _navMeshAgent.velocity.z);
    }

    private void TurnOnBoss()
    {
        _bossTrigger._bossTriggerFiredEvent -= TurnOnBoss;

        if (!IsAlive) return;

        _isStarted = true;
        StartCoroutine(SpawnDelayRoutine());
        StartCoroutine(RestoreHealthRoutine());
        StartCoroutine(AttackRoutine());
        _lastArtifact.SetActive(false);

    }
    private IEnumerator SpawnDelayRoutine()
    {
        yield return new WaitForSeconds(_spawnDelay / 3);

        while (IsAlive)
        {
            yield return new WaitForSeconds(_spawnDelay);
            SpawnAdditionalEnemies();
        }
    }

    private IEnumerator RestoreHealthRoutine()
    {
        while (IsAlive)
        {
            yield return new WaitForSeconds(_recoveryDelay);
            RestoreHealth(_maxHealth);
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (IsAlive)
        {
            if (_fieldOfView.CanSeePlayer)
            {
                _animator.SetBool(CritAttack, true);
                _unitSounds.PlayCritAttach();
            }
            yield return new WaitForSeconds(_attackPause);
        }
    }

    private void SpawnAdditionalEnemies()
    {
        if (!IsAlive) return;

        foreach (var point in _additSpawnPoints)
        {
            if (!IsAlive) return;
            var enemy = _enemiesFactory.SpawnEnemie(point);
            _enemiesSpawner.EnterParams(enemy, point);
            enemy.SetPimary(false);
        }
    }

    protected override void Death()
    {
        if (!IsAlive) return;
        _healthBar.SetActive(false);
        _text.SetActive(true);
        _visCon.SetActive(false);
        _lastArtifact.SetActive(true);
        _navMeshAgent.destination = transform.position;
        base.Death();

        var t = _deathDelay;
        while (t > 0)
        {
            t -= Time.deltaTime;
        }
    }

    public void Destroy()
        =>gameObject.SetActive(false);

    private void OnAttack()
    {
        if (_fieldOfView.CanSeePlayer)
            _player.SetDamage(_critDamage);
    }


    private void AttackEnd()
        => _animator.SetBool(CritAttack, false);
}
