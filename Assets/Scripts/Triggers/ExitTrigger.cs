using UnityEngine;
using Zenject;

public class ExitTrigger : BaseTrigger
{
    [Inject]
    private LVLLoader _lVLLoader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            _lVLLoader.LoadNextScene();
    }
}
