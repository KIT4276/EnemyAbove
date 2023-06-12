using UnityEngine;
using Zenject;

public class BetweenLVLFirstTrigger : BaseTrigger
{
    [SerializeField]
    private BetweenLVLSystem _betweenLVLCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            _betweenLVLCanvas.OpenText();
            
    }
}
