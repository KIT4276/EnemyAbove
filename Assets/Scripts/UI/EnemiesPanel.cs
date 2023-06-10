using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemiesPanel : MonoBehaviour
{
    private Text _text;
    private int _enemiesCount;

    [Inject]
    private EnemiesFactory _enemiesFactory;
    [Inject]
    private LVLLoader _lVLLoader;

    private void Start()
    {
        _text = GetComponent<Text>();
        _enemiesFactory.ChangeEnemiesCount += UpdateEnemy;
        _lVLLoader.SimpleLoadSceneCompliteEvent += UpdateEnemy;
    }

    private void UpdateEnemy()
    {
        _enemiesCount = _enemiesFactory.GetEnemiesCount();
        _text.text = _enemiesCount.ToString();
    }

    private void OnDestroy()
    {
        _enemiesFactory.ChangeEnemiesCount -= UpdateEnemy;
        _lVLLoader.SimpleLoadSceneCompliteEvent -= UpdateEnemy;
    }
}
