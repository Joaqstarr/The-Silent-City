using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Note 
{
        public enum NoteType
        {
            None = 0,
            F = 1,
            A = 2,
            C = 3,
            E = 4
        }
        public AudioClip _clip;
        public NoteType type;
        public UnityEvent OnPressed;
        public float PressedTime = 0;
        public bool IsPressed = false;
        public int id;
}
