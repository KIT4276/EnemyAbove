using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExitTrigger : BaseTrigger
{
    [Inject]
    private LVLLoader _lVLLoader;
    //[Inject]
    //private UpgradeMenu _upgradeMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _lVLLoader.LoadNextScene();
            //_upgradeMenu.gameObject.SetActive(true);
        }
    }
}
