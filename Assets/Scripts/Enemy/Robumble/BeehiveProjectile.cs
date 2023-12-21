using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeehiveProjectile : MonoBehaviour, IProjectile
{
    [SerializeField]SpriteRenderer _beehive;
    [SerializeField] Vector2 _beehiveStartX;
    [SerializeField] Vector2 _beehiveStartY;
    [SerializeField] float _time = 1.3f;
    [SerializeField] float _stayTime = 1.3f;
    [SerializeField] float _timeMultiplier = 1f;
    [Header("Do Shake Pos Settings")]
    [SerializeField] float _shakePosTime = 0.3f;
    [SerializeField] float _shakePosStrength = 0.1f;
    public void Initialize(BattleArena arena, Transform player, SongEval song)
    {
        transform.localPosition = Vector2.zero;
        _beehive.transform.localPosition = GetRandomPos();
        StartCoroutine(WaitForAppear(_time *  _timeMultiplier));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;


    Vector3[] points = new Vector3[4]
        {
                new Vector3(_beehiveStartX.x, _beehiveStartY.x, 0),
                new Vector3(_beehiveStartX.x, _beehiveStartY.y, 0),
                new Vector3(_beehiveStartX.y, _beehiveStartY.y, 0),
                new Vector3(_beehiveStartX.y, _beehiveStartY.x, 0)
        };
        transform.TransformPoints(points);

        Gizmos.DrawLineStrip(points, true);
    }

    private Vector2 GetRandomPos()
    {
        return new Vector2(Random.Range(_beehiveStartX.x, _beehiveStartX.y), Random.Range(_beehiveStartY.x, _beehiveStartY.y));
    }

    IEnumerator WaitForAppear(float time)
    {
        yield return new WaitForSeconds(time);
        _beehive.transform.DOShakePosition(_shakePosTime, _shakePosStrength).onComplete += OnShake;
    }

    private void OnShake()
    {
        Color col =_beehive.color;
        col.a = 1;
        _beehive.color = col;
        StartCoroutine(WaitToDestroy(_stayTime * _timeMultiplier));
    }
    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
        
    }
}
