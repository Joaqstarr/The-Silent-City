using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ScriptableObject
{
    [SerializeField] float _attackTimer = 10f;
    public virtual void StartAttack(BattleArena arena, Transform player, Action onFinish, EnemyAi ai, SongEval song)
    {
        ai.StartCoroutine(EndAttack(onFinish));
    }

    IEnumerator EndAttack(Action onFinish)
    {
        yield return new WaitForSeconds(_attackTimer);
        if(onFinish != null) 
        onFinish();
    }
}
