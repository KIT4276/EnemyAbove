using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

public class CameraPoint : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _bringCloserTime = 1;
    [SerializeField]
    private Transform _camera; 

    [Inject]
    private Player _target;

    public event SimpleHandle BringCloserEvent;

    private void FixedUpdate()
        => transform.position = Vector3.Lerp(transform.position, _target.transform.position, _speed * Time.fixedDeltaTime);

    public void BringCloser(Vector3 position)
        => StartCoroutine(BringCloserRoutine(position));

    private IEnumerator BringCloserRoutine(Vector3 position)
    {
        transform.DOMove(new Vector3(transform.position.x, position.y, position.z), _bringCloserTime);
        yield return new WaitForSeconds(_bringCloserTime);
        BringCloserEvent?.Invoke();
    }

    public Vector3 GetCameraTransform()
    {
        if (_camera == null) return Vector3.zero;
        else return _camera.position;
    }
}
