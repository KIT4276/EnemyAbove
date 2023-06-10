using UnityEngine;
using Zenject;
using DG.Tweening;

public class CoolDownBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform _background;
    [SerializeField]
    private RectTransform _fill;

    [Inject]
    private Player _player;

    private void OnEnable()
        => _player.PlayerFireEvent += FillingBar;

    private void FillingBar()
    {
        if(_fill == null)
            return;

        _fill.DOScaleX(6, (_player.GetCoolDown()) / 3);
        _fill.DOScaleX(0.01f, 2 * (_player.GetCoolDown()) / 3);
    }

    private void OnDestroy()
       => _player.PlayerFireEvent -= FillingBar;
}
