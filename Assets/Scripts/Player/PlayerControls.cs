using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Vector2 _moveInput;
    [SerializeField] private GameManager.GameState _usableState;
    private float _spawnTimer = 0;
    [SerializeField] Vector2 _ranSpawnTime;
    [SerializeField] WorldSectionManager _sectionManager;
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
        _spawnTimer = Random.Range(_ranSpawnTime.x, _ranSpawnTime.y);
    }
    public Vector2 GetMoveInput()
    {

        return _moveInput;
    }
    private void Update()
    {
        if (transform.parent != null) return;
        if(_moveInput.magnitude > 0 && _usableState == GameManager.instance.CurrentGameState)
        {
            _spawnTimer -= Time.deltaTime;
            if(_spawnTimer < 0 )
            {
                SpawnEnemy();
            }
        }
        
    }
    private void SpawnEnemy()
    {
        if (_usableState != GameManager.instance.CurrentGameState) return;
        _spawnTimer = Random.Range(_ranSpawnTime.x, _ranSpawnTime.y);
        _sectionManager.SpawnRandomEnemy();
        
    }
}
