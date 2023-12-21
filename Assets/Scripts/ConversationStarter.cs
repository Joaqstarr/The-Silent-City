using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] DialogueLine _startingLine;
    [SerializeField] UnityEvent _dialogueComplete;

    public void BeginDialogue()
    {
        DialogueSystem.instance.StartDialogue(_startingLine, _dialogueComplete);
    }
}
