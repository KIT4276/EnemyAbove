using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : BaseUnit
{
    protected Vector3 _centrePoint;
    protected Vector3 _cameraTransform;

    [SerializeField]
    protected NavMeshAgent _navMeshAgent;
    protected bool _primary;

    [SerializeField]
    protected float _deathDelay = 2.5f;
    [SerializeField]
    protected FieldOfView _fieldOfView;

    [Inject]
    protected EnemiesFactory _enemiesFactory;
    [Inject]
    protected DropLootSystem _dropLootSystem;
    [Inject]
    protected CameraPoint _cameraPoint;
    [Inject]
    protected Player _player;

    public bool IsInPool { get; private set; }

    public event SimpleHandle DeadEnemyEvent;

    private void OnEnable()
        =>_sideType = SideType.Enemy;

    protected void Update()
    {
        
    }

    public void AddCAmera(Vector3 value)
        => _cameraTransform = value;

    protected override void Death()
    {
        if (IsAlive)
        {
            _navMeshAgent.destination = transform.position;
            base.Death();
            StartCoroutine(DeathRoutin());
        }
    }

    protected IEnumerator DeathRoutin()
    {
        yield return new WaitForSeconds(_deathDelay);

        if (GetComponent<Boss>() == null && _primary)
            _dropLootSystem.DropLoot(this.transform);

        DeadEnemyEvent?.Invoke();
        Despawn();
    }

    protected void UpdateAnim()
    {
        _animator.SetFloat(SideMove, _navMeshAgent.velocity.x);
        _animator.SetFloat(ForvardMove, _navMeshAgent.velocity.z);
    }

    public void Despawn()
    {
        _navMeshAgent.enabled = false;
        _enemiesFactory.RemoveEnemy(this);
        transform.position = _centrePoint;
        IsInPool = true;
    }

    public void Restart()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (!_primary)
        {
            _navMeshAgent.SetDestination(_player.GetPlayersPos());
        }

        IsInPool = false;
        IsAlive = true;
        _currentHealth = _maxHealth;
        _canShot = true;
        _isShotPossible = true;
        _weaponClass.Restart();
    }

    public void NavMeshEnabled(bool value)
        =>_navMeshAgent.enabled = value;

    public void SetCentralPoint(Vector3 point)
        => _centrePoint = point;

    public void SetPimary(bool value)
        => _primary = value;

    public virtual void Detection() { }

    public class Pool : MonoMemoryPool<Enemy> { }
}
