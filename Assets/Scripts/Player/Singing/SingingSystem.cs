using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[Serializable]

struct NoteInfo
{
    public Note.NoteType type;
    public float heldTime;
    public bool pressedThisBeat;
    public AudioClip clip;
    //[HideInInspector]
    public int id;
}

public struct SongEval
{
    public int Quarter;
    public int Eighth;
    public int Half;
    public int Whole;
    public SongEval(NoteObject[] song)
    {
        Quarter = 0; 
        Eighth = 0; 
        Half = 0; 
        Whole = 0;


        for(int i = 0; i < song.Length; i++)
        {
            if (song[i] != null)
            {
                if (song[i]._length == SingingSystem.NoteLength.Eighth)
                    Eighth++;
                else if (song[i]._length == SingingSystem.NoteLength.Quarter || song[i]._length == SingingSystem.NoteLength.Quarterdot)
                    Quarter++;
                else if (song[i]._length == SingingSystem.NoteLength.Half || song[i]._length == SingingSystem.NoteLength.HalfDot)
                    Half++;
                else if (song[i]._length == SingingSystem.NoteLength.Whole)
                    Whole++;
            }
        }
    }

}

[RequireComponent(typeof(SingingInput))]
public class SingingSystem : MonoBehaviour
{
    public delegate void SongEvaluation(SongEval eval);
    public static SongEvaluation OnSongEval;

    public enum NoteLength
    {
        Eighth = 1,
        Quarter = 2,
        Quarterdot = 3,
        Half = 4,
        HalfDot = 6,
        Whole = 8
    }
    [SerializeField]
    int _bpm = 90;
    [SerializeField] float _playheadDistance = 0;
    [SerializeField] Transform _playhead;
    private Vector2 _playheadPosition;
    [SerializeField] private AudioSource _metronome;
    [SerializeField] private AudioSource _notePlayer;

    private bool _isPlaying;
    SingingInput _input;
    private float _duration = 0;

    [SerializeField] NoteObject _noteObject;
    [SerializeField] float _minimumHoldTime = 0.2f;
    [SerializeField]
    private NoteInfo[] _notesMap = new NoteInfo[8];
    public NoteObject[] _noteObjects = new NoteObject[8];


    [SerializeField] private float _startingNoteHeight = -62.5f;
    [SerializeField] private float _noteOffset = 10.1f;

    PlayerInput _playerInput;


    private EventHolder _onFinish;
    private bool _receivingInput = false;
    CombatSystem _combatSystem;

    [Header("Tutorial")]
    [SerializeField] bool _tutorial = false;
    [SerializeField] AudioSource _tutorialSource;
    private Action _finishAction;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _notesMap.Length; i++)
        {
            _notesMap[i] = new NoteInfo();
        }
        if (_playhead != null)
        {
            _playheadPosition = _playhead.localPosition;
        }
        _input = GetComponent<SingingInput>();
        _playerInput = GetComponent<PlayerInput>();
        if(transform.parent != null)
        _combatSystem = transform.parent.GetComponent<CombatSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isPlaying)
        {
            Note isPlaying = _input.GetActiveNote();
            if (isPlaying != null) 
                StartPlay();

        }
        else
        {
            _duration += Time.deltaTime;
            Note active = _input.GetActiveNote();
            if (_notesMap[GetCurrentBeat()].type == Note.NoteType.None && active != null && _receivingInput)
            {
                SetNoteByIndex(GetCurrentBeat(), active);
            }
            else
            {
                if(_noteObjects[GetCurrentBeat()] == null && _notesMap[GetCurrentBeat()].type == Note.NoteType.None && _receivingInput && _notePlayer.isPlaying)
                {
                    _notePlayer.Stop();
                }
            }
        }
    }
    public void StartPlay()
    {

        for(int i = 0; i < _notesMap.Length; i++)
        {
            _notesMap[i] = new NoteInfo();
            if (_noteObjects[i]!=null)
                Destroy(_noteObjects[i].gameObject);
            _noteObjects[i] = null;
        }
        _duration = 0;

        SetNoteByIndex(0, _input.GetActiveNote());
        _receivingInput = true;
        _isPlaying = true;
        _playhead.localPosition = _playheadPosition;
        if(_tutorial)
        {
            _tutorialSource.Play();
        }
        StartCoroutine(StartPlayhead(4));

        StartCoroutine(StartMetronome(4));
    }
    IEnumerator StartPlayhead(int amount)
    {
        
        for (int i = 0; i < (amount *2)-1; i++)
        {
            yield return new WaitForSeconds((60f / (_bpm *2)));
            _playhead.localPosition = new Vector3(_playhead.localPosition.x + ((_playheadDistance / 5) / 2), _playhead.localPosition.y, _playhead.localPosition.z);
            

        }

    }
    IEnumerator StartMetronome(int amount)
    {
        for (int i=0; i<amount; i++)
        {
            _metronome.Play();
            yield return new WaitForSeconds((60f / _bpm));
        }
        _receivingInput = false;

        _notePlayer.Stop();
        yield return new WaitForSeconds((60f / _bpm));

        SongEval evaluation = new SongEval(_noteObjects);

        if (_finishAction != null)
            _finishAction();

        if(OnSongEval != null)
            OnSongEval(evaluation);


    }

    private int GetCurrentBeat()
    {
        
        int curBeat = Mathf.RoundToInt(_duration / (60f / _bpm) * 2);
        //Debug.Log(curBeat);
        curBeat = Mathf.Clamp(curBeat, 1, 8);
        return curBeat-1;
    }
    public bool IsSinging()
    {
        if (!_isPlaying) return false;

        if(_notesMap[GetCurrentBeat()].type != Note.NoteType.None)
            return true;

        return false;
    }
    private void SetNoteByIndex(int index, Note setNote)
    {
        NoteInfo info = new NoteInfo();
        info.type = setNote.type;
        info.heldTime = setNote.PressedTime;
        info.id = setNote.id;
        info.clip = setNote._clip;
        if (index > 0)
            info.pressedThisBeat = (info.id != _notesMap[index-1].id);
        else
            info.pressedThisBeat = true;

        if (!info.pressedThisBeat)
            if ((info.heldTime % (60f / _bpm * 2)) < _minimumHoldTime)
                return;
        _notesMap[index] = info;
        if(info.pressedThisBeat)
            SpawnNote(setNote.type, index,setNote._clip);

    }

    private void SpawnNote(Note.NoteType type, int beat, AudioClip clip)
    {
        if (_noteObject == null) throw new Exception("No Note prefab set");
        PlayNote(clip);
        NoteObject spawnedNote = Instantiate(_noteObject, transform.GetChild(0));
        spawnedNote._clip = clip;
        Vector3 spawnPosition = new Vector3();
        float beatOffset = (_playheadDistance / 5) / 2;
        spawnPosition.x = _playheadPosition.x + (beat * beatOffset);
        spawnPosition.y = _startingNoteHeight + ((int)type * _noteOffset);
        spawnedNote.Initialize(type, spawnPosition, this, beat);
        _noteObjects[beat] = spawnedNote;
        
    }

    public NoteLength GetLengthFromBeat(int beat)
    {
        if (_notesMap[beat].type == Note.NoteType.None)
        {
            Debug.LogError("Note doesn't exist in beat " + beat);
            return NoteLength.Quarter;
        }
        int id = _notesMap[beat].id;
        int length = 0;
        for(int i = beat; i < _notesMap.Length; i++)
        {
            if (_notesMap[i].id == id)
                length++;
            else
                break;
        }

        if (length == 5)
            length = 4;

        if (length == 7)
            length = 6;

        NoteLength noteLength = (NoteLength)length;


        return noteLength;

    }


    public void EnableSing(EventHolder onfinish = null, Action finishAction = null)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        _playerInput.enabled = true;
        _playhead.localPosition = _playheadPosition;
        _isPlaying = false;
        _onFinish = onfinish;
        _finishAction = finishAction;
    }

    public void DisableSing()
    {


        transform.GetChild(0).gameObject.SetActive(false);
        _playerInput.enabled = false;
        for (int i = 0; i < _notesMap.Length; i++)
        {
            _notesMap[i] = new NoteInfo();
            if (_noteObjects[i] != null)
                Destroy(_noteObjects[i].gameObject);
            _noteObjects[i] = null;
        }
        _isPlaying = false;

        if (_onFinish != null)
            _onFinish.Execute();
    }

    public void Playback(Action onFinish = null)
    {
        _receivingInput = false;
        _duration = 0;
        _playhead.localPosition = _playheadPosition;
        StartCoroutine(StartPlayback(4, onFinish));
    }

    IEnumerator StartPlayback(int amount, Action onFinish = null)
    {
        TriggerNoteEffect(_noteObjects[0], 0);
        
        for (int i = 0; i < (amount * 2) - 1; i++)
        {
            yield return new WaitForSeconds((60f / (_bpm * 2)));
            _playhead.localPosition = new Vector3(_playhead.localPosition.x + ((_playheadDistance / 5) / 2), _playhead.localPosition.y, _playhead.localPosition.z);
            TriggerNoteEffect(_noteObjects[i + 1], i+ 1);


        }
        yield return new WaitForSeconds((60f / (_bpm * 2)));
        _notePlayer.Stop();

        yield return new WaitForSeconds((60f / (_bpm * 2)) * 2f);
        if (onFinish != null)
            onFinish();
    }
    private void TriggerNoteEffect(NoteObject note, int index)
    {
        if (note == null)
        {
            if(_notesMap[index].type == Note.NoteType.None)
                _notePlayer.Stop();

            return;
        }
        PlayNote(note._clip);
        note.RevealPower();
        if (note._length == NoteLength.Whole)
        {
            PlayerInfoHolder.Instance.Heal();
        }

        if (note._length == NoteLength.Quarter || note._length == NoteLength.Quarterdot)
        {
            if (_combatSystem != null)
                _combatSystem.DamageEnemy(1);
        }
    }
    private void PlayNote(AudioClip clip)
    {
        _notePlayer.Stop();
        _notePlayer.clip = clip;
        _notePlayer.Play();
    }

    public void EighthNoteTutorial(Action onSuccess, Action onFail, AudioClip clip)
    {
        _tutorialSource.clip = clip;
        EnableSing(null, () =>
        {
            if(new SongEval(_noteObjects).Eighth == 8)
            {
                onSuccess();
            }
            else
            {
                onFail();
            }
        });
    }
    public void QuarterNoteTutorial(Action onSuccess, Action onFail, AudioClip clip)
    {
        _tutorialSource.clip = clip;

        EnableSing(null, () =>
        {
            if (new SongEval(_noteObjects).Quarter == 4)
            {
                onSuccess();
            }
            else
            {
                onFail();
            }
        });
    }
    public void HalfNoteTutorial(Action onSuccess, Action onFail, AudioClip clip)
    {
        _tutorialSource.clip = clip;

        EnableSing(null, () =>
        {
            if (new SongEval(_noteObjects).Half == 2)
            {
                onSuccess();
            }
            else
            {
                onFail();
            }
        });
    }

    public void WholeNoteTutorial(Action onSuccess, Action onFail, AudioClip clip)
    {
        _tutorialSource.clip = clip;

        EnableSing(null, () =>
        {
            if (new SongEval(_noteObjects).Whole == 1)
            {
                onSuccess();
            }
            else
            {
                onFail();
            }
            DisableSing();
        });
    }

}
