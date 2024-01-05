using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamisBeamAttack : MonoBehaviour, IProjectile
{
    [SerializeField]
    float[] _rotations;
    [SerializeField]
    Color[] _ranColors;
    [SerializeField]
    Transform _shooter;
    [SerializeField]
    SpriteRenderer _beam;
    [SerializeField]
    float _beamStayTime = 1f;
    [SerializeField] float _timeMultiplier = 1f;
    [SerializeField] float _timeMultiplierPerNote = 0.3f;
    [SerializeField] Collider2D _collider;

    [Header("Do Shake Pos Settings")]
    [SerializeField] float _shakePosTime = 0.3f;
    [SerializeField] float _shakePosStrength = 0.1f;
    public void Initialize(BattleArena arena, Transform player, SongEval song)
    {
        _timeMultiplier += _timeMultiplierPerNote * song.Half;
        transform.localPosition = Vector2.zero;
        float angle = 0;
        if(_rotations.Length > 0)
        {
            int ran = Random.Range(0, _rotations.Length);
            angle = _rotations[ran];
        }
        if(_ranColors.Length > 0)
        {
            _beam.color = _ranColors[Random.Range(0, _ranColors.Length)];
        }

        transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, angle));
        _shooter.DOShakePosition(_shakePosTime * _timeMultiplier, _shakePosStrength).onComplete += FireBeam;
    }

    private void FireBeam()
    {
        _beam.gameObject.SetActive(true);
        _beam.DOFade(0, _beamStayTime).onComplete += Clean;
        StartCoroutine(DisableCollider());

    }
    private void Clean()
    {
        Destroy(gameObject);
    }
    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.1f);
        _collider.enabled = false;
    }
}
