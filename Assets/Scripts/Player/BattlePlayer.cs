using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
    public float speed = 1f;
    public float speedBoostPerNote = 1f;
    Rigidbody2D _rb;
    PlayerControls _controls;
    public SongEval _eval;

    
    // Start is called before the first frame update
    void Start()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();
        if (_controls == null)
            _controls = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newSpeed = speed;
        newSpeed += speedBoostPerNote * _eval.Eighth;

        _rb.MovePosition((Vector2)transform.position + newSpeed * _controls.GetMoveInput() * Time.fixedDeltaTime);

    }
}
