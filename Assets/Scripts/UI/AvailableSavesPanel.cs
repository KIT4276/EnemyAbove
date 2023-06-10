using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AvailableSavesPanel : MonoBehaviour
{
    [SerializeField]
    private MenuSounds _menuSounds;

    [SerializeField]
    private Text _playersName;
    [SerializeField]
    private Text _lvl;
    [SerializeField]
    private Text _exp;
    [SerializeField]
    private Text _chosenName;


    [Inject]
    private SaveSystem _saveSystem;

    private void Start()
       => GetValues();

    public void ChoseName()
    {
        _menuSounds.PlayClik();
        _chosenName.text = _playersName.text;
    }

    public void GetValues()
    {
        _saveSystem.LoadGameData();
        GameData data = _saveSystem.LoadGameData();

        _playersName.text = _saveSystem.PlayersNameValue;
        _lvl.text = data.lvl.ToString();
        _exp.text = data.experience.ToString();
    }

    public void DownloadGame()
    {
        _menuSounds.PlayClik();
        _saveSystem.DownloadGame();
    }
}
