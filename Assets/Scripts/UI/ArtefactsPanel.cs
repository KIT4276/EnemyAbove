using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ArtefactsPanel : MonoBehaviour
{
    [SerializeField]
    private Text _artifactsTaken;
    [SerializeField]
    private Text _totalArtifacts;

    [Inject]
    private ArtefactsSystem _artefactsSystem;

    private void Start()
    {
        _artefactsSystem.ChangeArtifactsCountEvent += ArtifactsUpdate;
        ArtifactsUpdate();
    }

    private void ArtifactsUpdate()
    {
        _artifactsTaken.text = _artefactsSystem.GetArtefactsTaken().ToString();
        _totalArtifacts.text = _artefactsSystem.GetTotalArtefacts().ToString();
    }

    private void OnDestroy()
        => _artefactsSystem.ChangeArtifactsCountEvent -= ArtifactsUpdate;
}
