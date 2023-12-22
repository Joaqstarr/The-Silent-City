using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SmileyBall : MonoBehaviour, IProjectile
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float velocity = 5f;
    [SerializeField] Vector3 _adjustedAimPoint;
    [SerializeField] Collider2D _collider;
    [SerializeField] float _waitTime = 0.5f;
    [SerializeField] float _gravWaitTime = 0.5f;
    [SerializeField] float _spin = 1f;

    private float _speedModifier = 1f;
    [SerializeField] private float _speedChangePer = -0.15f;

    [Header("Do Shake Pos Settings")]
    [SerializeField] float _shakePosTime = 0.3f;
    [SerializeField] float _shakePosStrength = 0.1f;
    float _gravScale = 0f;
    public void Initialize(BattleArena arena, Transform player, SongEval song)
    {

        _speedModifier += _speedChangePer * song.Half;

        _rb.gravityScale *= _speedModifier;
        _gravScale = _rb.gravityScale;
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            _gravScale = -_gravScale;
        }

        _rb.gravityScale = 0;
        _rb.velocity = Vector2.zero;
        _collider.enabled = false;
        transform.position = arena.GetRandomPointOnLine();

        transform.DOShakePosition(_shakePosTime, _shakePosStrength);
        StartCoroutine(Bounce(transform.parent.TransformPoint(Vector3.zero)));
    }
    IEnumerator Bounce(Vector3 playerPos)
    {
        yield return new WaitForSeconds(_waitTime);
        _rb.AddForce((((Vector2)playerPos + (Vector2)_adjustedAimPoint) - (Vector2)transform.position).normalized * velocity * _speedModifier, ForceMode2D.Impulse);
        _rb.AddTorque(_spin, ForceMode2D.Impulse);
        StartCoroutine(EnableGrav());

    }
    IEnumerator EnableGrav()
    {
        yield return new WaitForSeconds(_gravWaitTime);
        _rb.gravityScale = _gravScale;
        _collider.enabled=true;

    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }


}
