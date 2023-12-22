using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] SingingSystem _singingSystem;
    [SerializeField] SpriteRenderer _blackVoid;
    [Header("Intro")]
    [SerializeField]
    DialogueLine _exposition;
    [SerializeField]
    DialogueLine _intro;
    [SerializeField] AudioSource _audioSource;

    [Header("Eighth")]
    [SerializeField]
    DialogueLine _eighthNoteStart;
    [SerializeField]
    DialogueLine _eighthNoteLoop;
    [SerializeField] AudioClip _eighthClip;

    [Header("Quarter")]
    [SerializeField]
    DialogueLine _quarterNoteStart;
    [SerializeField]
    DialogueLine _quarterNoteLoop;
    [SerializeField] AudioClip _quarterClip;


    [Header("Half")]
    [SerializeField]
    DialogueLine _halfNoteStart;
    [SerializeField]
    DialogueLine _halfNoteLoop;
    [SerializeField] AudioClip _halfClip;

    [Header("Whole")]
    [SerializeField]
    DialogueLine _wholeNoteStart;
    [SerializeField]
    DialogueLine _wholeNoteLoop;
    [SerializeField] AudioClip _wholeClip;

    // Start is called before the first frame update
    public void StartGame()
    {
        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            Intro();
            Debug.Log("startdone");
            _blackVoid.DOFade(0, 2f);

        });
        DialogueSystem.instance.StartDialogue(_exposition, nextEvent);
    }

    private void Intro()
    {
        Debug.Log("Intro");
        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            _audioSource.DOFade(0, 0.2f);

            Debug.Log("intro done");
            _singingSystem.EnableSing(null, () =>
            {
                EighthNote();
            });
        });
        DialogueSystem.instance.StartDialogue(_intro, nextEvent);
    }

    public void EighthNote()
    {
        _audioSource.DOFade(1, 0.2f);

        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            _audioSource.DOFade(0, 0.2f);

            _singingSystem.EighthNoteTutorial(() =>
            {
                QuarterNote();
            }, () =>
            {
                DialogueSystem.instance.StartDialogue(_eighthNoteLoop, nextEvent);

            }, _eighthClip);
        });
        DialogueSystem.instance.StartDialogue(_eighthNoteStart, nextEvent);
    }

    public void QuarterNote()
    {
        _audioSource.DOFade(1, 0.2f);

        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            _audioSource.DOFade(0, 0.2f);

            _singingSystem.QuarterNoteTutorial(() =>
            {
                HalfNote();
            }, () =>
            {
                DialogueSystem.instance.StartDialogue(_quarterNoteLoop, nextEvent);

            },_quarterClip);
        });
        DialogueSystem.instance.StartDialogue(_quarterNoteStart, nextEvent);
    }

    public void HalfNote()
    {
        _audioSource.DOFade(1, 0.2f);

        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            _audioSource.DOFade(0, 0.2f);

            _singingSystem.HalfNoteTutorial(() =>
            {
                WholeNote();
            }, () =>
            {
                DialogueSystem.instance.StartDialogue(_halfNoteLoop, nextEvent);

            }, _halfClip);
        });
        DialogueSystem.instance.StartDialogue(_halfNoteStart, nextEvent);
    }
    public void WholeNote()
    {
        _audioSource.DOFade(1, 0.2f);

        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            _audioSource.DOFade(0, 0.2f);

            _singingSystem.WholeNoteTutorial(() =>
            {
                WholeNote();
            }, () =>
            {
                DialogueSystem.instance.StartDialogue(_wholeNoteLoop, nextEvent);

            }, _wholeClip);
        });
        DialogueSystem.instance.StartDialogue(_wholeNoteStart, nextEvent);
    }
}
