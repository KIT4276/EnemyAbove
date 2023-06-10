using UnityEngine;

public class UnitSounds : MonoBehaviour
{
    [SerializeField]
    protected AudioClip _shotSound;
    [SerializeField]
    protected AudioClip _deathSound;


    public void PlayShot()
        => AudioSource.PlayClipAtPoint(_shotSound, transform.position);

    public void PlayDeath()
        => AudioSource.PlayClipAtPoint(_deathSound, transform.position);
}
