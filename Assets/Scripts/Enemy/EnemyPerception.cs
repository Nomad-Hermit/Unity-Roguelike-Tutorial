using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerception
{
    public static bool IsPlayerVisible(Vector2Int position) {
        bool isVisible = false;

        foreach (Vector2Int pos in FoV.GetEnemyFoV(position)) {
            if (MapManager.map[pos.x, pos.y].hasPlayer) {
                isVisible = true;
                break;
            }
        }

        return isVisible;
    }
}
