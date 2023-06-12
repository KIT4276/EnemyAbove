using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LVLLoader : MonoBehaviour
{
    public const string SCENE_0 = "LVL0";
    public const string SCENE_1 = "LVL1";
    public const string SCENE_2 = "LVL2";
    public const string SCENE_3 = "LVL3";
    public const string SCENE_4 = "LVL4";
    public const string SCENE_5 = "LVL5";
    public const string BetweenLVL = "BetweenLVL";

    private bool _isLoadiing;

    private string _currentSceneName;
    private string _nextSceneName;
    private string _previousSceneName;

    [Inject]
    private Fader _fader;
    [Inject]
    private Player _player;

    public event SimpleHandle LoadSceneEvent;
    public event SimpleHandle<string> LoadSceneCompliteEvent;
    public event SimpleHandle SimpleLoadSceneCompliteEvent;
    public event SimpleHandle LoadNextSceneEvent;



    private void Start()
        => _player.PlayersDeathEvent += LoadThistScene;

    private void LoadThistScene()
    {
        if (_isLoadiing) return;

        StartCoroutine(LoadThistSceneRoutine());
    }

    private IEnumerator LoadThistSceneRoutine()
    {
        yield return new WaitForSeconds(_player.GetDelayAfterDeath());

        _currentSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneRoutine(_currentSceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        _isLoadiing = true;
        _fader.gameObject.SetActive(true);
        LoadSceneEvent?.Invoke();

        var waitFading = true;
        _fader.FadeIn(() => waitFading = false);

        while (waitFading)
            yield return null;

        SimpleLoadSceneCompliteEvent?.Invoke();


        var async = SceneManager.LoadSceneAsync(sceneName);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            yield return null;

        _player.Restart();

        // --------------------------------- todo additional events when loading a new scene

        LoadSceneCompliteEvent?.Invoke(sceneName);


        if (sceneName == BetweenLVL || sceneName == SCENE_0 || sceneName == SCENE_5)
        {
            _player.ShootingBan();
            if(sceneName != BetweenLVL)
                _player.MovingingBan();
        }
        else
        {
            LoadNextSceneEvent?.Invoke();
            _player.ShootingPermits();
            _player.MovingingPermits();
        }

        async.allowSceneActivation = true;

        waitFading = true;
        _fader.FadeOut(() => waitFading = false);

        if (sceneName == SCENE_5)
            _player.ShootingBan();

        while (waitFading)
            yield return null;

        _isLoadiing = false;
    }

    public void LoadNextScene()
    {
        if (_isLoadiing) return;

        _currentSceneName = SceneManager.GetActiveScene().name;
        _nextSceneName = ReturnNextSceneName(_currentSceneName);

        if (_currentSceneName != BetweenLVL)
        {
            _player.ShootingPermits();

            _previousSceneName = _currentSceneName;


            if (_currentSceneName == _nextSceneName)
                throw new Exception("You are trying to load a scene that is already loaded");

            if (_currentSceneName == SCENE_0 || _currentSceneName == SCENE_4)
                StartCoroutine(LoadSceneRoutine(_nextSceneName));
            else
                StartCoroutine(LoadSceneRoutine(BetweenLVL));
        }
        else
        {
            StartCoroutine(LoadSceneRoutine(ReturnNextSceneName(_previousSceneName)));
            _player.ShootingBan();
        }
    }

    public string ReturnNextSceneName(string name)
    {
        string nextSceneName;
        switch (name)
        {
            case SCENE_0:
                nextSceneName = SCENE_1;
                break;
            case SCENE_1:
                nextSceneName = SCENE_2;
                break;
            case SCENE_2:
                nextSceneName = SCENE_3;
                break;
            case SCENE_3:
                nextSceneName = SCENE_4;
                break;
            case SCENE_4:
                nextSceneName = SCENE_5;
                break;
            case SCENE_5:
                nextSceneName = SCENE_0;
                break;
            default:
                nextSceneName = BetweenLVL;
                break;
        }
        return nextSceneName;
    }

    public void LoadScene(string name)
        => StartCoroutine(LoadSceneRoutine(name));

    public string GetCurrentSceneName()
        => _currentSceneName;

    public string GetPrevSceneName()
        => _previousSceneName;

    public string DetermineSceneNameNow()
        => SceneManager.GetActiveScene().name;


    private void OnDestroy()
        => _player.PlayersDeathEvent += LoadThistScene;
}
