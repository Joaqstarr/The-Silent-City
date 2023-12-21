using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DarnetteProjectile : MonoBehaviour, IProjectile
{
    bool init = false;
    Transform _player;
    [SerializeField]float _angleDif = 0.7f;
    [SerializeField]float _lineUpSpeed = 1.0f;
    [SerializeField] float _speedModifier = 1.0f;
    [SerializeField] float _speedSlowdownPerHalf = 1f;
    [Header("Dash Settings")]
    [SerializeField] float _moveAmount = 2f;
    [SerializeField] float _moveTime = 2f;
    [SerializeField] Ease _ease;


    [Header("Do Shake Scale Settings")]
    [SerializeField] float _shakeScaleTime = 2f;
    [SerializeField] float _shakeScaleStrength = 1f;
    [Header("Do Shake Pos Settings")]
    [SerializeField] float _shakePosTime = 0.4f;
    [SerializeField] float _shakePosStrength = 1f;
    
    public void Initialize(BattleArena arena, Transform player, SongEval song)
    {
        _speedModifier += _speedSlowdownPerHalf * song.Half;
        transform.position = arena.GetRandomPointOnLine();
        _player = player;
        Vector3 diff = (player.position - transform.position);


        float angle = Mathf.Atan2(diff.y, diff.x) + (_angleDif * _speedModifier);

        //Now we set our new rotation. 
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
        init = true;
        transform.DOShakeScale(_shakeScaleTime, _shakeScaleStrength);
    }
    private void Update()
    {
        if (!init) return;


        Vector3 diff = (_player.position - transform.position);


        float angle = Mathf.Atan2(diff.y, diff.x) + _angleDif;

        //Now we set our new rotation. 
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);

        if(_angleDif > 0)
        _angleDif -= Time.deltaTime * _lineUpSpeed;
        else if(_angleDif == 0)
        {
            BeginDashPrep();
        }else
        {
            _angleDif = 0;
        }
    }

    private void BeginDashPrep()
    {
        init = false;
        transform.DOShakePosition(_shakePosTime, _shakePosStrength * _speedModifier).onComplete += Dash;

    }
    private void Dash()
    {
        transform.GetChild(0).DOLocalMoveX(_moveAmount, _moveTime * _speedModifier).SetEase(_ease).onComplete+=GetRidOf;
    }
    private void GetRidOf()
    {
        Destroy(this.gameObject,0.3f);
    }
}
