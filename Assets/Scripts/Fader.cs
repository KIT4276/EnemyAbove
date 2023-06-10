using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public static bool IsFading { get; private set; }

    private Action _fadedInCallBack;
    private Action _fadedOutCallBack;


    private void HandleFadeInAnimationOver()
    {
        _fadedInCallBack?.Invoke();
        _fadedInCallBack = null;
        IsFading = false;
    }

    private void HandleFadeOutAnimationOver()
    {
        _fadedOutCallBack?.Invoke();
        _fadedOutCallBack = null;
        IsFading = false;
        this.gameObject.SetActive(false);
    }

    public void FadeIn(Action fadedInCallBack)
    {
        if (IsFading) return;
        _fadedInCallBack = fadedInCallBack;
        IsFading = true;
        _animator.SetBool("Faded", true);
    }

    public void FadeOut(Action fadedOutCallBack)
    {
        if (IsFading) return;
        _fadedOutCallBack = fadedOutCallBack;
        IsFading = true;
        _animator.SetBool("Faded", false);
    }
}
