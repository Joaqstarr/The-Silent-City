using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeehiveProjectile : MonoBehaviour, IProjectile
{
    [SerializeField]SpriteRenderer _beehive;
    [SerializeField]
    SpriteRenderer[] _sprites;
    [SerializeField] Vector2 _beehiveStartX;
    [SerializeField] Vector2 _beehiveStartY;
    [SerializeField] float _time = 1.3f;
    [SerializeField] float _stayTime = 1.3f;
    [SerializeField] float _timeMultiplier = 1f;
    [SerializeField] float _timeMultiplierPerNote = 0.5f;

    [SerializeField] List<PolygonCollider2D> _fillers = new List<PolygonCollider2D>();
    [Header("Do Shake Pos Settings")]
    [SerializeField] float _shakePosTime = 0.3f;
    [SerializeField] float _shakePosStrength = 0.1f;
    [SerializeField] AudioSource _audioSource;

    public void Initialize(BattleArena arena, Transform player, SongEval song)
    {
        _timeMultiplier += _timeMultiplierPerNote * song.Half;
        transform.localPosition = Vector2.zero;
        _beehive.transform.localPosition = GetRandomPos();
        StartCoroutine(WaitForAppear(_time *  _timeMultiplier));
        _sprites = GetComponentsInChildren<SpriteRenderer>();
        SetSpriteAlpha(0.5f);
        RanDestroyFiller();
        RanDestroyFiller();

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
        _audioSource.Play();
        SetSpriteAlpha(1f);
        EnableColliders();
        Destroy(gameObject, _stayTime);

    }
    private void EnableColliders()
    {
        for (int i = 0; i < _fillers.Count; i++)
        {
            _fillers[i].enabled = true;
        }
    }
    private void SetSpriteAlpha(float alpha)
    {
        for(int i = 0; i < _sprites.Length; i++)
        {
            if (_sprites[i] != null)
            {
                Color col = _sprites[i].color;
                col.a = alpha;
                _sprites[i].color = col;
            }
        }
    }
    private void RanDestroyFiller()
    {
        int ran = Random.Range(0, _fillers.Count);
        Destroy(_fillers[ran].gameObject);
        _fillers.RemoveAt(ran);
    }
}
