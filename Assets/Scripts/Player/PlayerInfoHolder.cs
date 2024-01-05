using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoHolder : MonoBehaviour
{
    public static PlayerInfoHolder Instance;
    public bool _defeatedGuard = false;
    public bool _defeatedBoss = false;

    public int _health;
    [SerializeField]int _maxHealth;
    [SerializeField] Vector2 _startLocation;
    [SerializeField] GameObject _guard;
    [SerializeField] GameObject _boss;

    [SerializeField] int _heal = 6;
    [SerializeField]
    PlayerBattleUI _battleUI;
    void Start()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        SaveData _loadedData = SaveHandler.LoadData();

        if (_loadedData != null)
        {
            _health = _loadedData._health;
            Vector2 pos = new Vector2();
            pos.x = _loadedData.pos[0];
            pos.y = _loadedData.pos[1];
            transform.position = pos;
            _defeatedGuard = _loadedData._defeatedGuard;
            _defeatedBoss = _loadedData._defeatedBoss;
            if (_defeatedGuard) Destroy(_guard);
            if (_defeatedBoss) Destroy(_boss);


            //Save Loaded

        }
        else
        {
            _health = _maxHealth;
            transform.position = _startLocation;
            _defeatedGuard = false;
            _defeatedBoss = false;
            SaveHandler.SaveData(this);

            //Save Created
        }
        _battleUI.UpdateHealth(_health, _maxHealth);
    }

    public void Heal()
    {
        _health = Mathf.Min(_health+_heal, _maxHealth);
        _battleUI.UpdateHealth(_health, _maxHealth);
        
    }
    public void UpdateHealth(int health)
    {
        _health = health;
        _battleUI.UpdateHealth(_health,_maxHealth);

    }

}
