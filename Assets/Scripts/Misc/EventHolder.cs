using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHolder : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;

    public void Execute()
    {
        Debug.Log("EXECUTING");
        if (_event != null)
            _event.Invoke();
        else
            Debug.Log("Nothing in event");
    }
}
