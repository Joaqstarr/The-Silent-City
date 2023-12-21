using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyAi : MonoBehaviour
{
    [SerializeField]
    EnemyData enemyData;
    public int _health;
    [SerializeField] Attack[] _attacks;

    [Header("Do Shake Pos Settings")]
    [SerializeField] float _shakePosTime = 0.4f;
    [SerializeField] float _shakePosStrength = 1f;
    public void InitializeFight()
    {
        
        if (enemyData == null)
            throw new System.Exception("No Enemy Data");


        _health = enemyData._maxHealth;
        UnityEvent startFight = new UnityEvent();
        startFight.AddListener(StartFight);

        if (enemyData._startLine != null)
        {
            DialogueSystem.instance.StartDialogue(enemyData._startLine, startFight);
            return;
        }

        startFight.Invoke();
    }

    public void StartFight()
    {
            Debug.Log("ff");
        CombatSystem.instance.AllowPlayerSing();
    }

    public void Attack(BattleArena arena, Transform player,SongEval song, Action onComplete)
    {
        if(_attacks.Length <= 0)
        {
            if(onComplete!= null)
                onComplete();
            return;
        }

        
        Attack ranAttack = _attacks[UnityEngine.Random.Range(0, _attacks.Length)];
        ranAttack.StartAttack(arena, player, onComplete, this, song);
    }
    public int Damage(int amt)
    {
        _health -= amt;
        _health = Mathf.Max(0, _health);
        transform.DOShakePosition(_shakePosTime, _shakePosStrength);
        return _health;
    }
    public void EndFight(UnityAction afterEnd = null)
    {
        UnityEvent endFight = new UnityEvent();
        endFight.AddListener(afterEnd);

        if (enemyData._endLine != null)
        {
            DialogueSystem.instance.StartDialogue(enemyData._endLine, endFight);return;
        }
        if(afterEnd != null)
            afterEnd();
    }
}
