using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemyTrigger : MonoBehaviour
{
    [SerializeField] EnemyData _enemyData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CombatSystem.instance.StartBattle(_enemyData);
    }
}
