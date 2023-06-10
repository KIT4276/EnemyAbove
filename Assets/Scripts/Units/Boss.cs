using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Boss : Enemy
{
    private bool _isStarted;

    private readonly int CritAttack = Animator.StringToHash("Attack");

    [SerializeField]
    private float _critDamage;

    [Space, SerializeField]
    private BossTrigger _bossTrigger;
    [SerializeField]
    private float _spawnDelay = 5;
    [SerializeField]
    private float _recoveryDelay = 10;
    [SerializeField]
    private float _attackPause;
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

    private void Start()
    {
        _primary = true;
        _fieldOfView = GetComponent<FieldOfView>();
        Restart();
        _isStarted = false;
        _bossTrigger._bossTriggerFiredEvent += TurnOnBoss;
        _centrePoint = transform.position;
    }

    private void FixedUpdate()
    {
        if (_isStarted && IsAlive)
            _navMeshAgent.destination = _cameraPoint.transform.position;
        UpdateAnim();
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
                _animator.SetBool(CritAttack, true);
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
        base.Death();

    }

    public void Destroy()
        => gameObject.SetActive(false);

    private void OnAttack()
    {
        if (_fieldOfView.CanSeePlayer)
            _player.SetDamage(_critDamage);
    }


    private void AttackEnd()
        => _animator.SetBool(CritAttack, false);
}
