using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class SaveSystem : MonoBehaviour
{
    private GameData _data;
    private string _json;
    private string _path;

    public string PlayersNameValue { get; private set; }

    [Inject]
    private LVLLoader _lVLLoader;
    [Inject]
    private Player _player;
    [Inject]
    private ExperienceSystem _experienceSystem;

    private void Start()
    {
        _path = Application.streamingAssetsPath + "/Data.json";
        _lVLLoader.SimpleLoadSceneCompliteEvent += SaveGameData;
    }

    public void DownloadGame()
    {
        LoadGameData();

        _player.SetMaxHP(_data.maxHealth);
        _player.Set—oolDown(_data.coolDown);
        _player.SetMoveSpeed(_data.moveSpeed);
        _player.GetComponent<PlayersWeapon>().SetStockAmmo(_data.stockAmmo);
        _experienceSystem.SetExp(_data.experience);

        _experienceSystem.IsLoadedGame(true);
        _lVLLoader.LoadScene(_lVLLoader.ReturnNextSceneName(_data.lvl));
    }

    public void SaveGameData()
    {
        _data = new(_lVLLoader, _player, _experienceSystem, PlayersNameValue);
        _json = JsonUtility.ToJson(_data);

        if (!File.Exists(_path))
            File.Create(_path);

        File.WriteAllText(_path, _json);
    }

    public GameData LoadGameData()
    {
        _json = File.ReadAllText(_path);

        _data = JsonUtility.FromJson<GameData>(_json);
        PlayersNameValue = _data.playersName;
        return _data;
    }

    public void SaveName(string name)
        => PlayersNameValue = name;

    private void OnDestroy()
        => _lVLLoader.SimpleLoadSceneCompliteEvent -= SaveGameData;
}
