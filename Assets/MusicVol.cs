using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVol : MonoBehaviour
{
    [SerializeField]
    CombatSystem _combat;
    [SerializeField] AudioSource _source;
    [SerializeField] float _minVol = 0;
    [SerializeField] float _maxVol;
    // Start is called before the first frame update
    void Start()
    {
        _maxVol = _source.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(_combat._activePhase == CombatSystem.CombatPhase.sing)
        {
            _source.volume = _minVol;
        }
        else
        {
            _source.volume = _maxVol;

        }
    }
}
