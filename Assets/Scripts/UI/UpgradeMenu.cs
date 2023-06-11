using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeMenu : MonoBehaviour
{
    private float _experience;
    private float _rateOfFireSumValue;
    private float _maxHPSumValue;
    private float _moveSpeedSumValue;
    private float _stockAmmoSumValue;
    private float _ultimateSumValue;
    private Color _normColor;

    [SerializeField]
    private MenuSounds _menuSounds;
    [SerializeField]
    private Text _experienceText;

    [Header("Price"), SerializeField]
    private Text _rateOfFirePrice;
    [SerializeField]
    private Text _maxHPPrice;
    [SerializeField]
    private Text _moveSpeedPrice;
    [SerializeField]
    private Text _stockAmmoPrice;


    [Space, SerializeField, Header("Buttons")]
    private Text _rateOfFireButton;
    [SerializeField]
    private Text _maxHPButton;
    [SerializeField]
    private Text _moveSpeedButton;
    [SerializeField]
    private Text _stockAmmoButton;

    [Space, SerializeField, Header("Sum")]
    private Text _rateOfFireSum;
    [SerializeField]
    private Text _maxHPSum;
    [SerializeField]
    private Text _moveSpeedSum;
    [SerializeField]
    private Text _stockAmmoSum;

    [Space, SerializeField]
    private Text _ultimateSum;

    [Inject]
    private UpgradeSystem _upgradeSystem;
    [Inject]
    private ExperienceSystem _experienceSystem;

    private void Start()
    {
        gameObject.SetActive(false);
        _normColor = _ultimateSum.color;
        Restart();
    }

    private void OnEnable()
        => Restart();

    private void Restart()
    {
        _experience = _experienceSystem.TotalExperience;
        _experienceText.text = _experience.ToString();
        UpdateButtonsText();
        UpdatePrice();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            UpdateSumText();
    }

    private void UpdatePrice()
    {
        _rateOfFirePrice.text = _upgradeSystem.GetDcreaseÑoolDownPrace().ToString();
        _maxHPPrice.text = _upgradeSystem.GetIncreaseMaxHPPrace().ToString();
        _moveSpeedPrice.text = _upgradeSystem.GetMoveSpeedPrace().ToString();
        _stockAmmoPrice.text = _upgradeSystem.GetIncreaseStockAmmoPrace().ToString();
    }

    private void UpdateButtonsText()
    {
        _rateOfFireButton.text = "Ñêîðîñòðåëüíîñòü + " + _upgradeSystem.GetDecreaseÑoolDownValue().ToString();
        _maxHPButton.text = "Çàïàñ çäîðîâüÿ + " + _upgradeSystem.GetIncreaseMaxHP().ToString();
        _moveSpeedButton.text = "Ñêîðîñòü ïåðåäâèæåíèÿ  + " + _upgradeSystem.GetIncreaseMoveSpeed().ToString();
        _stockAmmoButton.text = "Çàïàñ ïàòðîíîâ + " + _upgradeSystem.GetIncreaseStockAmmo().ToString();
    }

    private void UpdateSum()
    {
        _ultimateSumValue = _rateOfFireSumValue + _maxHPSumValue + _moveSpeedSumValue + _stockAmmoSumValue;

        UpdateSumColor();

        _rateOfFireSum.text = _rateOfFireSumValue.ToString();
        _maxHPSum.text = _maxHPSumValue.ToString();
        _moveSpeedSum.text = _moveSpeedSumValue.ToString();
        _stockAmmoSum.text = _stockAmmoSumValue.ToString();
        _ultimateSum.text = _ultimateSumValue.ToString();
    }

    private void UpdateSumText()
    {
        _rateOfFireSum.text = _rateOfFireSumValue.ToString();
        _maxHPSum.text = _maxHPSumValue.ToString();
        _moveSpeedSum.text = _moveSpeedSumValue.ToString();
        _stockAmmoSum.text = _stockAmmoSumValue.ToString();
        _ultimateSum.text = _ultimateSumValue.ToString();
        UpdateSumColor();
    }

    private void UpdateSumColor()
    {
        if (_ultimateSumValue > _experienceSystem.TotalExperience)
            _ultimateSum.color = Color.red;
        else _ultimateSum.color = _normColor;
    }

    #region ForButtons

    public void UpdateRateOfFireSumValue()
    {
        _menuSounds.PlayClik();

        if (_rateOfFireSumValue == _upgradeSystem.GetDcreaseÑoolDownPrace())
            _rateOfFireSumValue = 0;
        else
            _rateOfFireSumValue = _upgradeSystem.GetDcreaseÑoolDownPrace();

        UpdateSum();
    }

    public void UpdateMaxHPSumValue()
    {
        _menuSounds.PlayClik();

        if (_maxHPSumValue == _upgradeSystem.GetIncreaseMaxHPPrace())
            _maxHPSumValue = 0;
        else
            _maxHPSumValue = _upgradeSystem.GetIncreaseMaxHPPrace();

        UpdateSum();
    }

    public void UpdateMoveSpeedSumValue()
    {
        _menuSounds.PlayClik();

        if (_moveSpeedSumValue == _upgradeSystem.GetMoveSpeedPrace())
            _moveSpeedSumValue = 0;
        else
            _moveSpeedSumValue = _upgradeSystem.GetMoveSpeedPrace();

        UpdateSum();
    }

    public void UpdateStockAmmoSumValue()
    {
        _menuSounds.PlayClik();

        if (_stockAmmoSumValue == _upgradeSystem.GetIncreaseStockAmmoPrace())
            _stockAmmoSumValue = 0;
        else
            _stockAmmoSumValue = _upgradeSystem.GetIncreaseStockAmmoPrace();

        UpdateSum();
    }

    public void Buy()
    {
        _menuSounds.PlayClik();

        if (_ultimateSumValue > _experienceSystem.TotalExperience)
        {
#if UNITY_EDITOR
            Debug.Log("íå äîñòàòî÷íî îïûòà");
#endif
            return;
        }

        var exp = _experienceSystem.ChangeExp(_ultimateSumValue);

        _ultimateSumValue = 0;
        string zero = "0";
        _rateOfFireSum.text = zero;
        _maxHPSum.text = zero;
        _moveSpeedSum.text = zero;
        _stockAmmoSum.text = zero;

        _experienceText.text = exp.ToString();

        _upgradeSystem.UpgradeMaxHP();
        _upgradeSystem.UpgradeRateOfFire();
        _upgradeSystem.UpgradeMoveSpeed();
        _upgradeSystem.Upgrade_stockAmmo();
        UpdateSum();
    }

    public void ResetSumm()
    {
        _menuSounds.PlayClik();
        _rateOfFireSumValue = 0;
        _maxHPSumValue = 0;
        _moveSpeedSumValue = 0;
        _stockAmmoSumValue = 0;
        _ultimateSumValue = 0;

        UpdateSumText();
    }

    public void CloseUpgradeMenu()
    {
        _menuSounds.PlayClik();
        this.gameObject.SetActive(false);
    }

    #endregion ForButtons
}
