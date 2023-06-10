using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class OffText : MonoBehaviour
{
    [Inject]
    private Player _player;

    private void Start()
    {
        _player.GetControls().PlayerActionMap.Attack.performed += Off;
        _player.GetControls().PlayerActionMap.WeaponSwitch.performed += Off;
        _player.GetControls().PlayerActionMap.Move.performed += Off;
    }

    private void Off(InputAction.CallbackContext obj)
    {
        _player.GetControls().PlayerActionMap.Attack.performed -= Off;
        _player.GetControls().PlayerActionMap.WeaponSwitch.performed -= Off;
        this.gameObject.SetActive(false);
    }
}
