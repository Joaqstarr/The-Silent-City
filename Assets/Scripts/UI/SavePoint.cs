using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    PlayerInfoHolder holder;
    [SerializeField]DialogueLine saveLine;
    bool canSave =false;
    private void Start()
    {
        StartCoroutine(WaitToEnable());
    }
    IEnumerator WaitToEnable()
    {
        yield return new WaitForSeconds(1f);
        canSave = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (!canSave) return;
        if (GameManager.instance.CurrentGameState != GameManager.GameState.overworld) return;

        if (holder == null)
            holder = GameObject.Find("OverworldPlayer").GetComponent<PlayerInfoHolder>();    

        SaveHandler.SaveData(holder);
        DialogueSystem.instance.StartDialogue(saveLine);
    }
}
