using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    CanvasGroup _dieScreen;
    [SerializeField]
    AudioSource _source;

    public void Die()
    {
        _dieScreen.alpha = 1;
        _dieScreen.interactable = true;
        _dieScreen.blocksRaycasts = true;
        _source.Play();
    }
}
