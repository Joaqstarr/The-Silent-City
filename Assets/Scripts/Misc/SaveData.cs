using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData 
{
    public float[] pos;
    public int _health;
    public bool _defeatedGuard;
    public SaveData(PlayerInfoHolder player) {
        pos = new float[2];
        pos[0] = player.transform.position.x;
        pos[1] = player.transform.position.y;
        _defeatedGuard = player._defeatedGuard;
        _health = player._health;

    }
}
