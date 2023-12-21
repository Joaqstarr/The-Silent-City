using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NoteObject : MonoBehaviour
{
    private Note.NoteType _type;
    Transform _rectTransform;
    SingingSystem _singingSystem;
    int _beat;
    [SerializeField] SpriteRenderer[] _noteArts;
    [SerializeField] SingingSystem.NoteLength[] _lengths;

    public SingingSystem.NoteLength _length = SingingSystem.NoteLength.Eighth;


    private Transform _art;
    [Header("Animation")]
    [SerializeField] float _growSpeed = 0.1f;
    [SerializeField] float _scaleShake = 0.1f;
    [SerializeField] float _scaleShakeTime = 0.1f;
    [SerializeField] float _shake = 0.1f;
    [SerializeField] float _shakeTime = 0.1f;
    [SerializeField] int _shakeVibrato = 40;
    [Header("Status Effect")]
    [SerializeField] SpriteRenderer _status;
    [SerializeField] Sprite[] _statusEffects;
    public LineRenderer _line;
    public AudioClip _clip;
    public void Initialize(Note.NoteType type, Vector3 position, SingingSystem system, int startingBeat)
    {
        _rectTransform = GetComponent<Transform>();

        _rectTransform.localPosition = position;
        _type = type;
        _singingSystem = system;
        InvokeRepeating("UpdateArt", 0, 0.4f);
        _beat = startingBeat;
        _art = transform.GetChild(0).GetComponent<Transform>();
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(scale, _growSpeed).SetEase(Ease.OutBack);

    }

    private void UpdateArt()
    {
        SingingSystem.NoteLength newLength = _singingSystem.GetLengthFromBeat(_beat);
        if(newLength != _length)
        {
            _length = newLength;
           // Debug.Log(newLength);
            if (_length != SingingSystem.NoteLength.Eighth)
            ShowArtBasedOnLength(_length);


        }
        if(_length == SingingSystem.NoteLength.Eighth)
        {
            if(_beat % 2 == 0 && _singingSystem._noteObjects[_beat +1] != null)
            {
                if (_singingSystem._noteObjects[_beat + 1]._length == SingingSystem.NoteLength.Eighth)
                {
                    
                    ShowArtBasedOnLength(SingingSystem.NoteLength.Quarter);
                    _line.enabled = true;
                    _line.SetPosition(0, _line.transform.position);
                    _line.SetPosition(1, _singingSystem._noteObjects[_beat + 1]._line.transform.position);

                }
                else
                {

                    ShowArtBasedOnLength(_length);

                    _line.enabled = false;

                }
            }
            else
            {
                if (_beat > 0 )
                    if(_singingSystem._noteObjects[_beat - 1] != null)
                    {
                        if (_singingSystem._noteObjects[_beat - 1]._length == SingingSystem.NoteLength.Eighth && _beat % 2 != 0)
                        {
                            ShowArtBasedOnLength(SingingSystem.NoteLength.Quarter);
                        }
                        else
                            ShowArtBasedOnLength(_length);
                    }
                    else
                        ShowArtBasedOnLength(_length);
                else
                    ShowArtBasedOnLength(_length);


            }
        }

    }

    private void ShowImage(int index)
    {
        if (_noteArts[index].enabled == true) return;

        _art.DOShakeScale(_scaleShakeTime, _scaleShake);
        _art.DOShakePosition(_shakeTime, _shake, _shakeVibrato);
        for(int i = 0; i < _noteArts.Length; i++)
        {
            _noteArts[i].enabled = false;
        }
        _noteArts[index].enabled = true;
    }

    private void ShowArtBasedOnLength(SingingSystem.NoteLength lengthToShow)
    {
        for (int i = 0; i < _lengths.Length; i++)
        {
            if (_lengths[i] == lengthToShow)
            {
                ShowImage(i);
                break;
            }
        }
    }

    public void RevealPower()
    {
        _art.DOShakeScale(_scaleShakeTime, _scaleShake);
        _art.DOShakePosition(_shakeTime, _shake, _shakeVibrato);
        switch (_length)
        {
            case SingingSystem.NoteLength.Eighth:
                _status.sprite = _statusEffects[0];
                break;
            case SingingSystem.NoteLength.Quarter:
            case SingingSystem.NoteLength.Quarterdot:
                _status.sprite = _statusEffects[1];

                break;
            case SingingSystem.NoteLength.Half:
            case SingingSystem.NoteLength.HalfDot:
                _status.sprite = _statusEffects[2];


                break;
            case SingingSystem.NoteLength.Whole:
                _status.sprite = _statusEffects[3];


                break;
        }

        _status.gameObject.SetActive(true);
        
    }
}
    