using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Vector2 _moveInput;
    [SerializeField] private GameManager.GameState _usableState;
    // Start is called before the first frame update

    public void OnMove(InputValue value)
    {
        if (_usableState != GameManager.instance.CurrentGameState)
        {
            _moveInput = Vector2.zero;
            return;
        }
        _moveInput = value.Get<Vector2>();
    }
    private void OnEnable()
    {
        _moveInput = Vector2.zero;
    }
    public Vector2 GetMoveInput()
    {

        return _moveInput;
    }
}
