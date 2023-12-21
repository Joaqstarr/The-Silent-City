using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
public class Fader : MonoBehaviour
{
    public static Fader instance;
    Image _image;
    [SerializeField] float _fadeTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        _image = GetComponent<Image>();

    }

    public void FadeUnfade(float time = 0.5f, Action onHalf = null, Action onDone = null)
    {
        GameManager.instance.SwitchState(GameManager.GameState.cutscene);
        _image.DOFade(1f, _fadeTime).OnComplete(() =>
        {
            if (onHalf != null)
                onHalf.Invoke();

            StartCoroutine(WaitBeforeUnfade(time, onDone));
        });
    }
    IEnumerator WaitBeforeUnfade(float time, Action onDone = null)
    {
        yield return new WaitForSeconds(time);

        _image.DOFade(0f, _fadeTime).OnComplete(()=>{
            GameManager.instance.SwitchToLastState();

            if(onDone != null)
                onDone.Invoke();

        });
    }

    public void CutToUnfade(float time = 0.6f)
    {
        GameManager.instance.SwitchState(GameManager.GameState.cutscene);

        Color color = _image.color; 
        color.a = 1f;
        _image.color = color;
        StartCoroutine(
        WaitBeforeUnfade(time));


    }
}
