using System.Collections;
using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{
    private float _distance;
    private Vector3 _startPos;
    [SerializeField]
    private ProjectileType _projectileType;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _lifeTime = 3;
    [SerializeField]
    private float _minDamage;
    [SerializeField]
    private float _maxDamage;
    [SerializeField, Tooltip("–ассто€ние перехода от ближней атаки к дальней")]
    private float _transitDistance;

    [Inject]
    private ProjectileFactory _factory;

    public float Damage { get; private set; }

    public SideType SideType { get; set; }

    private void OnEnable()
    {
        _startPos = transform.position;
        StartCoroutine(DestroyProjectileRoutine());
    }

    private void Update()
        => MoveProjectile();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseUnit>() != null || other.GetComponent<Projectile>() != null) return;
        else _factory.RemoveProjectile(this);
    }

    private void MoveProjectile()
    {
        transform.position += _speed * Time.deltaTime * transform.forward;
        _distance = Vector3.Distance(_startPos, transform.position);

        UpdateProjectileProperties();
    }

    private void UpdateProjectileProperties()
    {
        switch (_projectileType)
        {
            case ProjectileType.Tear:
                if (_distance < _transitDistance) Damage = _maxDamage;
                else Damage = _minDamage;
                break;
            case ProjectileType.Further:
                if (_distance <= _transitDistance) Damage = _minDamage;
                else Damage = _maxDamage;
                break;
        }
    }

    private IEnumerator DestroyProjectileRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        _factory.RemoveProjectile(this);
    }

    public void SetProjectileType(ProjectileType type)
        => _projectileType = type;

    public ProjectileType GetProjectileType()
        => _projectileType;

    public class Pool : MonoMemoryPool<Projectile> { }
}
