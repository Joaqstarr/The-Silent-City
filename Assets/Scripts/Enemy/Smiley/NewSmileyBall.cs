using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSmileyBall : MonoBehaviour, IProjectile
{
    [SerializeField]
    Vector2[] _spawnPoints;
    [SerializeField]
    Vector2[] _spawnVels;
    [SerializeField]
    Rigidbody2D _ball;
    private float _speedModifier = 1f;
    [SerializeField] private float _speedChangePer = -0.15f;
    public void Initialize(BattleArena arena, Transform player, SongEval song)
    {
        _speedModifier += _speedChangePer * song.Half;
        _ball.gravityScale *= _speedModifier;

        int ran = Random.Range(0, _spawnPoints.Length);
        _ball.transform.localPosition = _spawnPoints[ran];
        _ball.velocity = _spawnVels[ran ] * _speedModifier;

    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }

}
