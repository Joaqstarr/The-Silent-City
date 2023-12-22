using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int _maxHealth = 10;
    public int _health;
    public float _iframes;
    private float _iframeTimer = 0;
    [SerializeField] UnityEvent OnDamage;
    [SerializeField] DeathScreen _death;

    private void OnEnable()
    {
        _health = PlayerInfoHolder.Instance._health;
        _iframeTimer = 0;
    }

    private void Update()
    {
        if(_iframeTimer > 0)
            _iframeTimer -= Time.deltaTime;
    }
    public void Damage()
    {
        if(_iframeTimer > 0) return;
        if (_health <= 0) return;
        _health -= 1;
        PlayerInfoHolder.Instance.UpdateHealth(_health);
        _iframeTimer = _iframes;
        OnDamage.Invoke();
        if(_health <= 0)
        {
            _death.Die();
            GameManager.instance.SwitchState(GameManager.GameState.cutscene);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TRIGGER ENTER");
        if (collision.CompareTag("ProjectileAttack"))
        {
            Damage();
        }
    }
}
