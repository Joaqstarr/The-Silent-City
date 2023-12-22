using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldSectionManager : MonoBehaviour
{
    [SerializeField] WorldSection[] WorldSections;
    [SerializeField] bool _drawDebug = true;
    [SerializeField] Transform _player;

    WorldSection activeSection;
    void Update()
    {
        activeSection = GetSectionFromPosition(_player.position);
        if(activeSection != null)
        {
            if (activeSection._camera.Priority != 10)
            {
                for (int i = 0; i < WorldSections.Length; i++)
                {
                    if (WorldSections[i] == activeSection)
                        activeSection._camera.Priority = 10;
                    else
                        WorldSections[i]._camera.Priority = 0;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!_drawDebug) return;
        
        for (int i = 0; i < WorldSections.Length; i++)
        {
            Gizmos.color = WorldSections[i]._debugColor;


            Vector3[] points = new Vector3[4]
            {
                new Vector3(WorldSections[i]._xBounds.x, WorldSections[i]._yBounds.x, 0),
                new Vector3(WorldSections[i]._xBounds.x, WorldSections[i]._yBounds.y, 0),
                new Vector3(WorldSections[i]._xBounds.y, WorldSections[i]._yBounds.y, 0),
                new Vector3(WorldSections[i]._xBounds.y, WorldSections[i]._yBounds.x, 0)
            };
            Gizmos.DrawLineStrip(points, true);
        }
    }

    public WorldSection GetSectionFromPosition(Vector3 posToCheck)
    {
        for (int i = 0;i < WorldSections.Length;i++)
        {
            if (WorldSections[i]._xBounds.x < posToCheck.x && posToCheck.x < WorldSections[i]._xBounds.y && WorldSections[i]._yBounds.x < posToCheck.y && posToCheck.y < WorldSections[i]._yBounds.y)
                return WorldSections[i];
        }
        return null;
    }

    public void SpawnRandomEnemy()
    {
        if (activeSection == null) return;
        CombatSystem.instance.StartBattle(activeSection._enemyPool[Random.Range(0, activeSection._enemyPool.Length)]);
    }
}
