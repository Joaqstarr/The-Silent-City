using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] DialogueLine _line;
    [SerializeField] UnityEvent _event;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.SwitchState(GameManager.GameState.cutscene);
            DialogueSystem.instance.StartDialogue(_line, _event);

        }
    }



    public void SendToCredits()
    {
        Fader.instance.FadeUnfade(0.5f, () =>
        {
            SceneManager.LoadScene(2);
        });
    }
}
