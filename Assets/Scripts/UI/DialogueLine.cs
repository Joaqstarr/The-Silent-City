using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialogue Line")]
public class DialogueLine : ScriptableObject
{
    [Header("Content")]
    public string Line;
    public string Speaker;
    [Header("Audio")]
    public bool SoundOneShot;
    public AudioClip Sound ;
    [Header("Settings")]
    public bool AutoContinue;
    public float Speed = 0.01f;


    [Space][Space]

    public DialogueLine Next;






}
