using UnityEngine;
using Zenject;

public class ArtefactsSystem : MonoBehaviour
{
    private static int _artifactsTaken;
    private static int _totalArtifacts;

    [SerializeField]
    private int _artifactsOnLVL1 = 1;
    [SerializeField]
    private int _artifactsOnLVL2 = 3;
    [SerializeField]
    private int _artifactsOnLVL3 = 5;
    [SerializeField]
    private int _artifactsOnLVL4 = 5;

    [Inject]
    private LVLLoader _lVLLoader;
    [Inject]
    private Player _player;

    public event SimpleHandle ChangeArtifactsCountEvent;

    private void Start()
    {
        SettingArtifacts(_lVLLoader.DetermineSceneNameNow());

        _lVLLoader.LoadSceneCompliteEvent += SettingArtifacts;
        _player.GetArtifactEvent += ArtifactTaken;
    }

    private void SettingArtifacts(string name)
    {
        _artifactsTaken = 0;

        switch (name)
        {
            case "LVL1":
                _totalArtifacts = _artifactsOnLVL1;
                break;
            case "LVL2":
                _totalArtifacts = _artifactsOnLVL2;
                break;
            case "LVL3":
                _totalArtifacts = _artifactsOnLVL3;
                break;
            case "LVL4":
                _totalArtifacts = _artifactsOnLVL4;
                break;
        }

        ChangeArtifactsCountEvent?.Invoke();
    }

    public void ArtifactTaken()
    {
        _totalArtifacts--;
        _artifactsTaken++;

        ChangeArtifactsCountEvent?.Invoke();

        if (_totalArtifacts == 0)
            _lVLLoader.LoadNextScene();
    }

    public int GetArtefactsTaken()
        => _artifactsTaken;

    public int GetTotalArtefacts()
        => _totalArtifacts;
}
