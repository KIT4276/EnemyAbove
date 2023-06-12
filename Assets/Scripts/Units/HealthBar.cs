using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [Inject]
    private CameraPoint _cameraPoint;

    [SerializeField]
    private BaseUnit _npc;
    [SerializeField]
    private Slider _healthBar;

    private void Start()
    {
        _healthBar.maxValue = _npc.GetMaxHealth();
    }

    private void FixedUpdate()
    {
        transform.LookAt(_cameraPoint.GetCameraTransform());
        _healthBar.value = _npc.GetCurrentHealth();
    }
}
