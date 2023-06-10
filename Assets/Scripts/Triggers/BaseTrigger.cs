using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrigger : MonoBehaviour
{
    protected Collider _collider;

    [SerializeField]
    protected float _value;
    [SerializeField]
    protected TriggerType triggerType;

    protected void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    public float GetValue()
        => _value;

    public TriggerType GetTriggerType()
        => triggerType;
}
