using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileyBounceSound : MonoBehaviour
{
    [SerializeField]AudioSource m_AudioSource;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("SmileyFloor"))
            m_AudioSource.Play();
    }
}
