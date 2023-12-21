using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour
{
    

    public GameObject _arena;
    public Transform _player;
    BattlePlayer _pbattle;

    private Vector2 _playerPos;
    [SerializeField] private EdgeCollider2D _edgeCollider;
    private void Start()
    {
        _playerPos = _player.position;
        _pbattle = _player.GetComponent<BattlePlayer>();
    }

    public void StartAttack(SongEval song)
    {
        _pbattle._eval = song;
        _player.position = _playerPos;
        _arena.SetActive(true);
    }
    public void EndAttack()
    {
        _arena.SetActive(false);
    }

    public Vector2 GetRandomPointOnLine()
    {
        int ranSide = Random.Range(0, 4);
        float lerp = Random.Range(0f, 1f);

        Vector2 side = Vector2.Lerp(_edgeCollider.points[ranSide], _edgeCollider.points[ranSide + 1], lerp);
        
        side = _edgeCollider.transform.TransformPoint(side);
        return side;
    }
}
