using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int _maxHealth = 10;
    public Sprite _portrait;
    public string _name;

    public DialogueLine _startLine;
    public DialogueLine _endLine;
    public DialogueLine[] _hitLine;
    public EnemyAi _enemyAi;

}
