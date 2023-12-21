using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    string _name = " ";
    int _maxHealth = 5;
    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        CombatSystem.InitializeFightScreen += InitializeUI;
        CombatSystem.ResetBattleScreen += DisableBattleUi;
        CombatSystem.OnDamageEnemy += UpdateHealth;

    }
    private void OnDisable()
    {
        CombatSystem.InitializeFightScreen -= InitializeUI;
        CombatSystem.ResetBattleScreen -= DisableBattleUi;
        CombatSystem.OnDamageEnemy -= UpdateHealth;

    }

    [SerializeField] TMP_Text _nameText;
    CanvasGroup _canvasGroup;
    private void InitializeUI(EnemyData data)
    {
        _canvasGroup.alpha = 1;
        _name = data.name;
        _maxHealth = data._maxHealth;
        _nameText.text = _name + "\nHP: " + data._maxHealth+"/"+_maxHealth;
    }


    public void UpdateHealth(int health)
    {
        _nameText.text = _name + "\nHP: " + health + "/" + _maxHealth;

    }

    void DisableBattleUi()
    {

        _canvasGroup.alpha = 0;

    }
}
