using UnityEngine;
using Zenject;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField, Header("Upgrade Value")]
    private float _decreaseÑoolDownValue = 0.1f;
    [SerializeField]
    private float _increaseMaxHP = 10;
    [SerializeField]
    private float _increaseMoveSpeed = 0.5f;
    [SerializeField]
    private float _increaseStockAmmo = 10;

    [Space, SerializeField, Header("Upgrade Prace")]
    private float _decreaseÑoolDownPrace = 20;
    [SerializeField]
    private float _increaseMaxHPPrace = 10;
    [SerializeField]
    private float _moveSpeedPrace = 15;
    [SerializeField]
    private float _increaseStockAmmoPrace = 19;

    [Inject]
    private Player _player;

    #region UpgradeMethods

    public void UpgradeRateOfFire()
        => _player.DecreaseÑoolDown(_decreaseÑoolDownValue);

    public void UpgradeMaxHP()
        => _player.IncreaseMaxHP(_increaseMaxHP);

    public void UpgradeMoveSpeed()
        => _player.IncreaseMoveSpeed(_increaseMoveSpeed);

    public void Upgrade_stockAmmo()
        => _player.GetComponent<PlayersWeapon>().IncreaseStockAmmo(_increaseStockAmmo);

    #endregion UpgradeMethods

    #region GetValuees

    public float GetDecreaseÑoolDownValue()
        => _decreaseÑoolDownValue;

    public float GetIncreaseMaxHP()
        => _increaseMaxHP;

    public float GetIncreaseMoveSpeed()
        => _increaseMoveSpeed;

    public float GetIncreaseStockAmmo()
        => _increaseStockAmmo;

    public float GetDcreaseÑoolDownPrace()
        => _decreaseÑoolDownPrace;

    public float GetIncreaseMaxHPPrace()
        => _increaseMaxHPPrace;

    public float GetMoveSpeedPrace()
        => _moveSpeedPrace;

    public float GetIncreaseStockAmmoPrace()
        => _increaseStockAmmoPrace;

    #endregion GetValuees
}
