using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Linq;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _dialogueText;
    UnityEvent _OnCompleteEvent;
    DialogueLine _currentLine;

    [SerializeField]
    CanvasGroup _boxGroup;

    [Header("Fade Anim")]
    [SerializeField] float _fadeTime = 0.2f;

    public static DialogueSystem instance;
    private void Awake()
    {
        if(instance != null)
            Destroy(this);

        instance = this;
    }
    public void StartDialogue(DialogueLine line, UnityEvent completeEvent = null)
    {
        Debug.Log(line.Line);
        if(_dialogueText == null)
        {
            Debug.LogError("NO TMP TEXT");
            return;
        }
        if(_boxGroup.interactable == false)
        {
            EnableBox();
        }
        _OnCompleteEvent = completeEvent;
        _currentLine = line;

        _dialogueText.SetText(line.Line);
        StartCoroutine(StartReveal(line.Speed, line.Line.Length));
    }


    public void EnableBox()
    {
        if (_boxGroup == null) throw new System.Exception("No Canvas Group Assigned");

        _boxGroup.DOFade(1, _fadeTime);
        _boxGroup.blocksRaycasts = true;
        _boxGroup.interactable = true;
    }

    public void DisableBox()
    {
        if (_boxGroup == null) throw new System.Exception("No Canvas Group Assigned");
        _boxGroup.DOFade(0, _fadeTime);
        _boxGroup.blocksRaycasts = false;
        _boxGroup.interactable = false;
    }

    IEnumerator StartReveal(float time, int count)
    {

        int total = count;
        for (int i = 0; i <= total; i++)
        {
            _dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(time);
        }
    }

    public void OnContinue()
    {
        if (_boxGroup == null) return;
        if (_boxGroup.blocksRaycasts == false) return;
        if (_dialogueText.maxVisibleCharacters < _currentLine.Line.Length - 1)
        {
            _dialogueText.maxVisibleCharacters = _currentLine.Line.Length;
            StopAllCoroutines();
            return;
        }

        if (_currentLine.Next != null)
        {
            StartDialogue(_currentLine.Next, _OnCompleteEvent);
            return;
        }
        DisableBox();

        if (_OnCompleteEvent != null) 
        _OnCompleteEvent.Invoke();
    }
}
