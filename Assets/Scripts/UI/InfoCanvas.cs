using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InfoCanvas : MonoBehaviour
{
    private int _number = 0;

    [SerializeField]
    private Text[] _texts;

    [Inject]
    private LVLLoader _loader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_number < _texts.Length - 1)
            {
                _texts[_number].gameObject.SetActive(false);
                _texts[_number + 1].gameObject.SetActive(true);
                _number++;
            }
        }
    }

    public void StartGame()
        => _loader.LoadNextScene();
}
