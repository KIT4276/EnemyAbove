using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartMenu : MonoBehaviour
{
    private MenuSounds _menuSounds;

    [SerializeField]
    private Canvas _startCanvas;
    [SerializeField]
    private Canvas _infoCanvas;
    [SerializeField]
    private Canvas _savesCanvas;
    [SerializeField]
    private Canvas _enterNameCAnvas;
    [SerializeField]
    private Canvas _availableSavesCanvas;

    [Space, SerializeField]
    private InputField _inputField;

    [Inject]
    private SaveSystem _saveSystem;

    private void Start()
        => _menuSounds = GetComponent<MenuSounds>();

    public void EnterNameCAnvasOn()
    {
        _menuSounds.PlayClik();
        _enterNameCAnvas.gameObject.SetActive(true);
        _startCanvas.gameObject.SetActive(false);
    }

    public void PreStartNewGame()
    {
        _menuSounds.PlayClik();
        _infoCanvas.gameObject.SetActive(true);
        _enterNameCAnvas.gameObject.SetActive(false);
    }

    public void OpenSaves()
    {
        _menuSounds.PlayClik();
        _startCanvas.gameObject.SetActive(false);
        _savesCanvas.gameObject.SetActive(true);
    }

    public void SaveNAme()
    {
        _menuSounds.PlayClik();
        string _name = _inputField.text;
        _saveSystem.SaveName(_name);
    }

    public void OpenAvailableSaves()
    {
        _menuSounds.PlayClik();
        _availableSavesCanvas.gameObject.SetActive(true);
        _savesCanvas.gameObject.SetActive(false);
    }

    public void Back()
    {
        _menuSounds.PlayClik();
        _savesCanvas.gameObject.SetActive(false);
        _enterNameCAnvas.gameObject.SetActive(false);
        _startCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        _menuSounds.PlayClik();
        Application.Quit();
    }
}
