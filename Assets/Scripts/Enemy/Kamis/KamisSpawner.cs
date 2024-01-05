using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KamisSpawner : MonoBehaviour
{
    [SerializeField] EnemyData _enemyData;
    [SerializeField] DialogueLine _dialogueLine;
    [SerializeField] UnityEvent _event;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if(_dialogueLine != null)
        {
            GameManager.instance.SwitchState(GameManager.GameState.cutscene);
            DialogueSystem.instance.StartDialogue(_dialogueLine, _event);
        }
        else
        {
            StartBattle();
        }

    }
    public void StartBattle()
    {
        CombatSystem.instance.StartBattle(_enemyData, () =>
        {
            PlayerInfoHolder.Instance._defeatedBoss = true;
            Destroy(gameObject);
        });
    }
}
