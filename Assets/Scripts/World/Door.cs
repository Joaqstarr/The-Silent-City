using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Vector2 _teleportPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.CurrentGameState != GameManager.GameState.overworld) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Fader.instance.FadeUnfade(0.5f, () =>
            {
                collision.transform.position = _teleportPos;

            });

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, _teleportPos);
        Gizmos.DrawWireSphere(_teleportPos, 0.5f);
    }

}
