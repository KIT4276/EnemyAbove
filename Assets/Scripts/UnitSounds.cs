using UnityEngine;

public class UnitSounds : MonoBehaviour
{
    [SerializeField]
    protected AudioClip _shotSound;
    [SerializeField]
    protected AudioClip _deathSound;
    [SerializeField]
    private AudioClip _critAttackSound;

    public void PlayShot()
        => AudioSource.PlayClipAtPoint(_shotSound, transform.position);

    public void PlayDeath()
        => AudioSource.PlayClipAtPoint(_deathSound, transform.position);

    public void PlayCritAttach()
        => AudioSource.PlayClipAtPoint(_critAttackSound, transform.position);
}
