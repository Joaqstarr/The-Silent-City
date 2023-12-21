using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SingingInput : MonoBehaviour
{


    public Note F;
    public Note A;
    public Note C;
    public Note E;

    public Note[] notes = new Note[4];

    // Start is called before the first frame update
    void Start()
    {
        F.type = Note.NoteType.F;
        A.type = Note.NoteType.A;
        C.type = Note.NoteType.C;
        E.type = Note.NoteType.E;


        notes[0] = F;
        notes[1] = A;
        notes[2] = C;
        notes[3] = E;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < notes.Length; i++)
        {
            notes[i].PressedTime = (notes[i].IsPressed) ? notes[i].PressedTime + Time.deltaTime : 0;
        }
    }
    public void OnFNote(InputValue value)
    {
        CheckNoteInput(value.Get<float>() > 0, F);
    }

    public void OnANote(InputValue value)
    {
        CheckNoteInput(value.Get<float>() > 0, A);
    }

    public void OnCNote(InputValue value)
    {
        CheckNoteInput(value.Get<float>() > 0, C);
    }

    public void OnENote(InputValue value)
    {
        CheckNoteInput(value.Get<float>()>0, E);
    }

    private void CheckNoteInput(bool newInput, Note n)
    {
        if (n.IsPressed == false && newInput)
        {
            if (n.OnPressed != null)
            {
                n.OnPressed.Invoke();
                n.PressedTime = 0;
                n.id = (int) Random.Range(0, 100000000);
            }
        }
        n.IsPressed = newInput;
    }
    public Note GetActiveNote(float maxTime = 10)
    {
        Note active = null;
        for (int i = 0; i < notes.Length; i++)
        {
            if (active == null)
            {
                if (notes[i].IsPressed)
                    active = notes[i];
            }
            else
            {
                if (notes[i].PressedTime > active.PressedTime)
                    active = notes[i];
            }
        }
        if (active == null)
            return null;
        if(active.PressedTime > maxTime)
            return null;

        return active;
    }
}
