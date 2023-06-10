using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisappearingText : MonoBehaviour
{
    private Text _text;

    [SerializeField]
    private float _hideTime = 3;

    private void OnEnable()
    {
        _text = GetComponent<Text>();
        StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        Color textColor = _text.color;
        textColor.a = 1;
        _text.color = textColor;

        float timer = _hideTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            textColor.a = (1f / _hideTime) * timer;
            _text.color = textColor;

            yield return null;
        }
    }
}
