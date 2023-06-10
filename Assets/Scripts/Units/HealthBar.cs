using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [Inject]
    private CameraPoint _cameraPoint;
    private Vector3 _camera;

    [SerializeField]
    private BaseUnit _npc;
    [SerializeField]
    private Slider _healthBar;

    private void Start()
    {
        _camera = new Vector3(_cameraPoint.GetCameraTransform().position.x, _cameraPoint.GetCameraTransform().position.y + 13.9f,
            _cameraPoint.GetCameraTransform().position.z);

        _healthBar.maxValue = _npc.GetMaxHealth();
    }

    private void Update()
    {
        transform.LookAt(_camera);
        _healthBar.value = _npc.GetCurrentHealth();
    }
}
