using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public void Initialize(BattleArena arena, Transform player, SongEval song);
}
