using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldSection
{
    [SerializeField] private string Name;
    public int id;
    public CinemachineVirtualCamera _camera;
    public Vector2 _xBounds;
    public Vector2 _yBounds;
    public Color _debugColor;
    public EnemyData[] _enemyPool;
    public AudioClip _music;

}
