using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    [Header("Outro")]
    [SerializeField] DialogueLine _outro;
    [SerializeField] float HillY;
    [SerializeField] float Building1;
    [SerializeField] float Building2;
    [SerializeField] Transform _hill;
    [SerializeField] Transform _building1;
    [SerializeField] Transform _building2;
    [SerializeField] float _transTime = 4f;
    [SerializeField]CanvasGroup _bg;
    [Header("Final Expo")]
    [SerializeField] DialogueLine _finalExpo;




    // Start is called before the first frame update
    public void StartGame()
    {
        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            Intro();
            _blackVoid.DOFade(0, 2f);

        });
        DialogueSystem.instance.StartDialogue(_exposition, nextEvent);
    }

    private void Intro()
    {
        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            _audioSource.DOFade(0, 0.2f);

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
                FinishTutorial();
            }, () =>
            {
                DialogueSystem.instance.StartDialogue(_wholeNoteLoop, nextEvent);

            }, _wholeClip);
        });
        DialogueSystem.instance.StartDialogue(_wholeNoteStart, nextEvent);
    }
    public void FinishTutorial()
    {
        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            Pan();
        });

        DialogueSystem.instance.StartDialogue(_outro, nextEvent);
    }
    private void Pan()
    {
        _hill.DOLocalMoveY(HillY, _transTime);
        _building1.DOLocalMoveY(Building1, _transTime);
        _building2.DOLocalMoveY(Building2, _transTime).onComplete+= FadeOut;
    }
    private void FadeOut()
    {
        _bg.DOFade(1, 1).onComplete += FinalExposition;
    }
    private void FinalExposition()
    {
        UnityEvent nextEvent = new UnityEvent();
        nextEvent.AddListener(() =>
        {
            ChangeScene();
        });

        DialogueSystem.instance.StartDialogue(_finalExpo, nextEvent);
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
