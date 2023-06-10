using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BetweenLVLSystem : MonoBehaviour
{
    [SerializeField]
    private Text _after1LVL;
    [SerializeField]
    private Text _after2LVL;
    [SerializeField]
    private Text _after3LVL;

    [Inject]
    private LVLLoader _lVLLoader;


    public void OpenText()
    {
        var sceneName = _lVLLoader.GetPrevSceneName();

        switch (sceneName)
        {
            case LVLLoader.SCENE_1:
                _after1LVL.gameObject.SetActive(true);
                _after2LVL.gameObject.SetActive(false);
                _after3LVL.gameObject.SetActive(false);
                break;
            case LVLLoader.SCENE_2:
                _after2LVL.gameObject.SetActive(true);
                _after1LVL.gameObject.SetActive(false);
                _after3LVL.gameObject.SetActive(false);
                break;
            case LVLLoader.SCENE_3:
                _after3LVL.gameObject.SetActive(true);
                _after1LVL.gameObject.SetActive(false);
                _after2LVL.gameObject.SetActive(false);
                break;

        }
    }
}
