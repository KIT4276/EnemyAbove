using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : BaseTrigger
{
    public event SimpleHandle _bossTriggerFiredEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            _bossTriggerFiredEvent?.Invoke();
    }
}
