using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource _clikSound;

    public void PlayClik()
       => _clikSound.Play();
}
