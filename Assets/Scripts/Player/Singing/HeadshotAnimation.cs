using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadshotAnimation : MonoBehaviour
{
    [SerializeField]
    SingingSystem _singing;

    [SerializeField]
    Animator _animator;

    private void Update()
    {
        _animator.SetBool("IsSinging", _singing.IsSinging());
    }
}
