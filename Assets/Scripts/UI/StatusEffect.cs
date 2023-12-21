using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StatusEffect : MonoBehaviour
{
    Vector3 _startPos;
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] float _duration;
    [SerializeField] float _height;
    private void OnEnable()
    {
        _sprite.color = Color.white;
        transform.localPosition = Vector3.zero;
        _sprite.DOFade(0, _duration).SetEase(Ease.InSine);
        transform.DOLocalMoveY(_height, _duration);


    }
}
