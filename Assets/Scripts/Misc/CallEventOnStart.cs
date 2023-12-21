using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEventOnStart : EventHolder
{
    // Start is called before the first frame update
    void Start()
    {
        Execute();
    }
}
