using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatSystem : MonoBehaviour
{
    public delegate void OnBattleStart(EnemyData data);
    public static OnBattleStart InitializeFightScreen;

    public delegate void OnBattleEnd();
    public static OnBattleEnd ResetBattleScreen;

    public delegate void Damage(int health);
    public static Damage OnDamageEnemy;
    private AudioSource _battleStartedSound;
    [SerializeField]
    CinemachineVirtualCamera _battleCam;
    SingingSystem _singingSystem;

    [Header("Enemy Spawning")]
    [SerializeField] Transform _enemyParent;
    public enum CombatPhase
    {
        start,
        dialogue,
        sing,
        enemyAttack,
        endscreen
        
    }

    CombatPhase _activePhase;
    EnemyAi _activeEnemy;
    [Header("Enemy Attacks")]

    [SerializeField] BattleArena _arena;

    public static CombatSystem instance;
    private void Start()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        _singingSystem = GetComponentInChildren<SingingSystem>();
        _battleStartedSound = GetComponent<AudioSource>();

    }

    private void OnEnable()
    {
        SingingSystem.OnSongEval += OnSung;

    }
    private void OnDisable()
    {
        SingingSystem.OnSongEval -= OnSung;
    }


    public void StartBattle(EnemyData _data)
    {
        GameManager.instance.SwitchState(GameManager.GameState.combat);

        Fader.instance.CutToUnfade();
        _battleStartedSound.Play();
        _battleCam.Priority = 20;
        _activeEnemy = Instantiate(_data._enemyAi,_enemyParent);
        InitializeFightScreen(_data);
        _activeEnemy.InitializeFight();
    }

    public void EndBattle()
    {
        GameManager.instance.SwitchState(GameManager.GameState.overworld);

        Fader.instance.FadeUnfade(0.5f, () =>
        {
            _battleCam.Priority = -1;
            ResetBattleScreen();
        });




    }
    public void SwitchState(CombatPhase phase)
    {
        _activePhase = phase;
    }

    public void AllowPlayerSing()
    {
        SwitchState(CombatPhase.sing);
        _singingSystem.EnableSing();
    }
    private void OnSung(SongEval song)
    {
        //vfx
        _singingSystem.Playback(() =>
        {
            BeginEnemyAttackPhase(song);
        });
    }

    public void DamageEnemy(int amt)
    {
         
        if(OnDamageEnemy != null)
        {
            OnDamageEnemy(_activeEnemy.Damage(amt));
        }
        else
        {
            _activeEnemy.Damage(amt);
        }
    }
    private void BeginEnemyAttackPhase(SongEval song)
    {
        _singingSystem.DisableSing();
        if(_activeEnemy._health <= 0)
        {
            EndScreen();
            return;
        }
        SwitchState(CombatPhase.enemyAttack);
        _arena.StartAttack(song);
        _activeEnemy.Attack(_arena, _arena._player, song,() =>
        {
            _arena.EndAttack();
             if(_activeEnemy._health <= 0)
            {
                EndScreen();
            }
            else
            {
                AllowPlayerSing();
            }
        });
    }
    private void EndScreen()
    {
        _activeEnemy.EndFight(() =>
        {
            EndBattle();
        });

    }

}