using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PausePanel : MonoBehaviour
{
    private bool _isPaused = false;
    private MenuSounds _menuSounds;

    [SerializeField]
    private Canvas _pauseCanvas;
    [SerializeField]
    private Canvas _mainGameCanvas;
    [SerializeField]
    private Canvas _exitCanvas;
    [SerializeField]
    private Button _button;

    [Inject]
    private SaveSystem _saveSystem;

    private void Start()
        => _menuSounds = GetComponent<MenuSounds>();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
                OnPause();
            else
                OfPause();
        }
    }

    private void OnPause()
    {
        _menuSounds.PlayClik();
        Time.timeScale = 0;
        _pauseCanvas.gameObject.SetActive(true);
        _mainGameCanvas.gameObject.SetActive(false);
        _isPaused = true;
    }

    public void OfPause()
    {
        _menuSounds.PlayClik();
        Time.timeScale = 1;
        _pauseCanvas.gameObject.SetActive(false);
        _mainGameCanvas.gameObject.SetActive(true);
        _isPaused = false;
    }

    public void OpetExitCanvas()
    {
        _menuSounds.PlayClik();
        _button.gameObject.SetActive(false);
        _exitCanvas.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _menuSounds.PlayClik();

        if (_saveSystem != null)
            _saveSystem.SaveGameData();

        Application.Quit();
    }
}
