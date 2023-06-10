using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    private Vector3 _camera;

    [Inject]
    private CameraPoint _cameraPoint;

    [SerializeField]
    private BaseUnit _npc;
    [SerializeField]
    private Slider _healthBar;

    private void Start()
    {
        _camera = _cameraPoint.GetCameraTransform();
        _healthBar.maxValue = _npc.GetMaxHealth();
    }

    private void Update()
    {
        transform.LookAt(_camera);
        _healthBar.value = _npc.GetCurrentHealth();
    }
}
