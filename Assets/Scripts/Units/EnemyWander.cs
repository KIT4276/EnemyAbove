using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWander : Enemy
{
    protected float _distance;
    

    [SerializeField]
    protected float _detectionDistance = 15f;
    [SerializeField]
    protected float _attackDistance = 10f;
    [SerializeField]
    private float _range;
    [SerializeField]
    private float _maxDistance = 1;

    private void FixedUpdate()
    { 
        OnMove();
    }
    

    private void OnMove()
    {
        if (!IsAlive) return;
#if UNITY_EDITOR
        if (!_navMeshAgent.isOnNavMesh) Debug.Log("!   navMeshAgent isnt OnNavMesh!   " + transform.position);
#endif
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            ChangeDirection();
        }

        UpdateAnim();
    }

    private void ChangeDirection()
    {
        if (!IsAlive) return;

        Vector3 point;
        if (RandomPoint(_centrePoint, _range, out point))
        {
#if UNITY_EDITOR
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
#endif 
            _navMeshAgent.SetDestination(point);
#if UNITY_EDITOR
            Debug.DrawLine(transform.position, point, Color.red, 1.0f);
#endif
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, _maxDistance, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private IEnumerator DetectionRoutine()
    {
        while (true)
        {
            _distance = Vector3.Distance(_player.GetPlayersPos(), transform.position);
            
            if (IsAlive)
            {
                if (_distance <= _detectionDistance)
                {
                    
                    _navMeshAgent.destination = _cameraPoint.transform.position;

                    if (_canShot && _fieldOfView.CanSeePlayer)
                        StartCoroutine(HoldShot());
                }
            }

            yield return null;
        }
    }

    protected override void Death()
    {
        if (!IsAlive) return;

        _canShot = false;
        base.Death();
    }

    public override void Detection()
    {
        StartCoroutine(DetectionRoutine());
    }
}
