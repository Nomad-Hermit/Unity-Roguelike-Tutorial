using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement 
{
    public static void Move(Vector2Int target, EnemyBrain brain) {

        if (MapManager.map[target.x, target.y].isWalkable && !MapManager.map[target.x, target.y].hasPlayer && !MapManager.map[target.x, target.y].hasEnemy) {
            MapManager.map[brain.position.x, brain.position.y].hasEnemy = false;
            MapManager.map[brain.position.x, brain.position.y].enemyID = -1;
            MapManager.map[brain.position.x, brain.position.y].enemyObject = null;

            brain.position = target;

            MapManager.map[brain.position.x, brain.position.y].hasEnemy = true;
            MapManager.map[brain.position.x, brain.position.y].enemyID = brain.id;
            MapManager.map[brain.position.x, brain.position.y].enemyObject = brain.gameObject;

            DungeonGenerator dungeon = GameObject.Find("GameManager").GetComponent<DungeonGenerator>();
            brain.gameObject.transform.position = new Vector3(brain.position.x * dungeon.tileScaling, brain.position.y * dungeon.tileScaling, brain.gameObject.transform.position.z);
        }
    }
}
