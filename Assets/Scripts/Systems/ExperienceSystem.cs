using UnityEngine;
using Zenject;

public class ExperienceSystem : MonoBehaviour
{
    private bool _isLoadedGame;
    private float _loadedExp;

    [SerializeField]
    private int _expForKilling;
    [SerializeField]
    private int _expForArtifact;


    [Inject]
    private LVLLoader _lVLLoader;
    [Inject]
    private Player _player;
    [Inject]
    private EnemiesFactory _enemiesFactory;

    public float ExperienceOnLVL { get; private set; }
    public float TotalExperience { get; private set; }

    public event SimpleHandle SummarizeExperiensEvent;

    private void Start()
    {
        _player.GetArtifactEvent += AddArtifactExperiens;
        _enemiesFactory.DeadSumEnemyEvent += AddKillingExperiens;
        _player.PlayersDeathEvent += RestartExperiens;
        _lVLLoader.SimpleLoadSceneCompliteEvent += SummarizeExperiens;
    }

    private void AddArtifactExperiens()
        => ExperienceOnLVL += _expForArtifact;

    private void AddKillingExperiens()
        => ExperienceOnLVL += _expForKilling;

    private void RestartExperiens()
        => ExperienceOnLVL = 0;

    private void SummarizeExperiens()
    {
        TotalExperience += ExperienceOnLVL;
        ExperienceOnLVL = 0;
        SummarizeExperiensEvent?.Invoke();


        if (_isLoadedGame)
        {
            TotalExperience = _loadedExp;
            _isLoadedGame = false;
        }
    }

    private void OnDestroy()
    {
        _player.GetArtifactEvent -= AddArtifactExperiens;
        _enemiesFactory.DeadSumEnemyEvent -= AddKillingExperiens;
        _player.PlayersDeathEvent -= RestartExperiens;
        _lVLLoader.LoadNextSceneEvent -= SummarizeExperiens;
    }

    public float ChangeExp(float value)
    {
        TotalExperience -= value;
        return TotalExperience;
    }

    public void SetExp(float value)
        => _loadedExp = value;

    public void IsLoadedGame(bool value)
        => _isLoadedGame = value;
}
