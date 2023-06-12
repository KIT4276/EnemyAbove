using UnityEngine;
using Zenject;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField, Header("Upgrade Value")]
    private float _decrease—oolDownValue = 0.1f;
    [SerializeField]
    private float _increaseMaxHP = 10;
    [SerializeField]
    private float _increaseMoveSpeed = 0.5f;
    [SerializeField]
    private float _increaseStockAmmo = 10;

    [Space, SerializeField, Header("Upgrade Prace")]
    private float _decrease—oolDownPrace = 20;
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
    {     
        _player.Decrease—oolDown(_decrease—oolDownValue);
        Debug.Log("UpgradeRateOfFire");
    }

    public void UpgradeMaxHP()
    { 
        _player.IncreaseMaxHP(_increaseMaxHP);
        Debug.Log("UpgradeMaxHP");
    }

    public void UpgradeMoveSpeed()
    {         
        _player.IncreaseMoveSpeed(_increaseMoveSpeed);
        Debug.Log("UpgradeMoveSpeed");
    }

    public void Upgrade_stockAmmo()
    { 
         _player.GetComponent<PlayersWeapon>().IncreaseStockAmmo(_increaseStockAmmo);
        Debug.Log("Upgrade_stockAmmo");
    }

    #endregion UpgradeMethods

    #region GetValuees

    public float GetDecrease—oolDownValue()
        => _decrease—oolDownValue;

    public float GetIncreaseMaxHP()
        => _increaseMaxHP;

    public float GetIncreaseMoveSpeed()
        => _increaseMoveSpeed;

    public float GetIncreaseStockAmmo()
        => _increaseStockAmmo;

    public float GetDcrease—oolDownPrace()
        => _decrease—oolDownPrace;

    public float GetIncreaseMaxHPPrace()
        => _increaseMaxHPPrace;

    public float GetMoveSpeedPrace()
        => _moveSpeedPrace;

    public float GetIncreaseStockAmmoPrace()
        => _increaseStockAmmoPrace;

    #endregion GetValuees
}
