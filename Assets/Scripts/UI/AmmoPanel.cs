using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AmmoPanel : MonoBehaviour
{
    private PlayersWeapon _playersWeapon;

    [SerializeField]
    private Text _ammoInMagText;
    [SerializeField]
    private Text _leftAmmoText;
    [SerializeField]
    private Image _tearWeaponImage;
    [SerializeField]
    private Image _furtherWeaponImage;

    [Inject]
    private Player _player;

    private void Start()
    {
        _playersWeapon = _player.GetPlayersWeapon();
        _playersWeapon.WeaponSwitchEvent += WeaponSwitch;
        _player.PlayerAmmoChangeEvent += AmmoUpdate;
        Restart();
    }

    private void WeaponSwitch()
    {
        switch (_playersWeapon.ActiveWeapon)
        {
            case ProjectileType.Tear:
                _tearWeaponImage.gameObject.SetActive(true);
                _furtherWeaponImage.gameObject.SetActive(false);

                break;
            case ProjectileType.Further:
                _tearWeaponImage.gameObject.SetActive(false);
                _furtherWeaponImage.gameObject.SetActive(true);
                break;
        }
        AmmoUpdate();
    }

    private void AmmoUpdate()
    {
        switch (_playersWeapon.ActiveWeapon)
        {
            case ProjectileType.Tear:
                _ammoInMagText.text = _playersWeapon.GetAmmoInMagTear().ToString();
                _leftAmmoText.text = _playersWeapon.GetLeftAmmoTear().ToString();
                break;
            case ProjectileType.Further:
                _ammoInMagText.text = _playersWeapon.GetAmmoInMagFurther().ToString();
                _leftAmmoText.text = _playersWeapon.GetLeftAmmoFurther().ToString();
                break;
        }
    }

    public void Restart()
    {
        _tearWeaponImage.gameObject.SetActive(true);
        _furtherWeaponImage.gameObject.SetActive(false);

        _ammoInMagText.text = _playersWeapon.GetMagazineCapacity().ToString();
        _leftAmmoText.text = _playersWeapon.GetLeftAmmoTear().ToString();
    }

    private void OnDestroy()
    {
        _playersWeapon.WeaponSwitchEvent -= WeaponSwitch;
        _player.PlayerAmmoChangeEvent -= AmmoUpdate;
    }
}
