using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : UnitSounds
{
    [SerializeField]
    private AudioClip _weaponSwitchSound;
    [SerializeField]
    private AudioClip _takeLootSound;
    [SerializeField]
    private AudioClip _takeArtifactSound;
    [SerializeField]
    protected AudioClip _stepSound;


    public void PlayWeaponSwitch()
        => AudioSource.PlayClipAtPoint(_weaponSwitchSound, transform.position);

    public void PlayTakeLoot()
        => AudioSource.PlayClipAtPoint(_takeLootSound, transform.position);

    public void PlayTakeArtifact()
        => AudioSource.PlayClipAtPoint(_takeArtifactSound, transform.position);
    public void Step()
        => AudioSource.PlayClipAtPoint(_stepSound, transform.position);
}
