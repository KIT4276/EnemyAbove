using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private float _radius;
    [SerializeField, Range(0, 360)]
    private float _angle;
    [SerializeField]
    private float _checkDelay = 0.2f;
    [SerializeField]
    private Transform _eye;
    //[SerializeField]
    //private GameObject _visionPrefab;

    private BaseUnit _unit;

    //[SerializeField]
    //private VisionCone vision;

    [Inject]
    public Player _player;

    public LayerMask targetMask;
    public LayerMask ObstacleMask;

    public bool CanSeePlayer { get; private set; }

    private void Start()
    {
        _unit = GetComponent<BaseUnit>();

        //vision = GetComponent<VisionCone>();

        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_checkDelay);

        while (_unit.IsAlive)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstacleMask))
                    CanSeePlayer = true;

                else
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else
            CanSeePlayer = false;
    }

    public float GetAngle()
        => _angle;

    public float GetRadius()
        => _radius;

    public Transform GetEye()
        => _eye;
}
