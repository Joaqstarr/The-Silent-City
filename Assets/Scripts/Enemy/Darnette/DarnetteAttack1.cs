using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
struct SpawnData
{
    public float time;
    public int amount;
}
[CreateAssetMenu(menuName ="Prefab Spawn Attack")]
public class DarnetteAttack1 : Attack
{
    [Header("Attack Spawns")]
    [SerializeField] GameObject _prefabToSpawn;
    [SerializeField] SpawnData[] _spawns;
    
    public override void StartAttack(BattleArena arena, Transform player, Action onFinish, EnemyAi ai, SongEval song)
    {
        base.StartAttack(arena, player, onFinish, ai, song);

        for (int i = 0; i < _spawns.Length; i++)
        {
            ai.StartCoroutine(SpawnAfterTime(_spawns[i], arena, player, song));
        }
    }

    IEnumerator SpawnAfterTime(SpawnData data, BattleArena arena, Transform player, SongEval song)
    {
        yield return new WaitForSeconds(data.time);

        for(int i = 0; i < data.amount; i++)
        {
            IProjectile proj = Instantiate(_prefabToSpawn, arena._arena.transform).GetComponent<IProjectile>();
            proj.Initialize(arena, player, song);
        }
    }
    
}
