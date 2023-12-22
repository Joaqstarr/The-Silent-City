using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    public float speed = 1f;
    Rigidbody2D _rb;
    PlayerControls _controls;
    [SerializeField] Animator _animator;

    
    // Start is called before the first frame update
    void Start()
    {
        if( _rb == null )
            _rb = GetComponent<Rigidbody2D>();
        if(_controls == null )
            _controls = GetComponent<PlayerControls>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        Vector2 input = _controls.GetMoveInput();
        _rb.MovePosition((Vector2)transform.position + speed * input * Time.fixedDeltaTime);
        _animator.SetFloat("InputX", input.x);
        _animator.SetFloat("InputY", input.y);

    }
}
