using UnityEngine;
using Zenject;

public class UpgradeTrigger : BaseTrigger
{
    [SerializeField]
    private Transform _closerTransform;

    [SerializeField]
    private Vector3 _afterUpgradePoint;
    [SerializeField]
    private AudioClip _actionSound;

    [Inject]
    private CameraPoint _cameraPoint;
    [Inject]
    private Player _player;
    [Inject]
    private UpgradeMenu _upgradeMenu;

    private void Start()
        =>_cameraPoint.BringCloserEvent += UpgradeMenuOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            AudioSource.PlayClipAtPoint(_actionSound, transform.position);
            _player = other.GetComponent<Player>();
            _cameraPoint.BringCloser(_closerTransform.position);
            _player.MovingingBan();
        }
    }

    private void UpgradeMenuOn()
    {
        _upgradeMenu.gameObject.SetActive(true);

        _player.transform.position = _afterUpgradePoint;
    }
}
