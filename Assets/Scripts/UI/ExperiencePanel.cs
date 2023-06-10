using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ExperiencePanel : MonoBehaviour
{
    [SerializeField]
    private Text _experienceOnLVLText;
    [SerializeField]
    private Text _totalExperienceText;

    [Inject]
    private Player _player;
    [Inject]
    private EnemiesFactory _enemiesFactory;
    [Inject]
    private ExperienceSystem _experienceSystem;


    private void Start()
    {
        UpdateExp();

        _player.GetArtifactEvent += UpdateExp;
        _enemiesFactory.DeadSumEnemyEvent += UpdateExp;
        _player.PlayersDeathEvent += UpdateExp;
    }

    private void UpdateExp()
    {
        _experienceOnLVLText.text = _experienceSystem.ExperienceOnLVL.ToString();
        _totalExperienceText.text = _experienceSystem.TotalExperience.ToString();
    }


    private void OnDestroy()
    {
        _player.GetArtifactEvent -= UpdateExp;
        _enemiesFactory.DeadSumEnemyEvent -= UpdateExp;
        _player.PlayersDeathEvent -= UpdateExp;
    }
}
