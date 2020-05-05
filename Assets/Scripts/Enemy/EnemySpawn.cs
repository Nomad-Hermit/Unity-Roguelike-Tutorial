using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;

    public void SpawnEnemy (Vector2Int position, float tileScaling) {
        GameObject enemyObject = Instantiate(prefab, new Vector3(position.x * tileScaling, position.y * tileScaling, -0.5f), Quaternion.identity);
        enemyObject.transform.parent = parent.transform;

        int dataID;
        Enemy enemy = new Enemy() { position = position, name = CallEnemy(out dataID), brain = enemyObject.GetComponent<EnemyBrain>() };
        enemyObject.name = enemy.name;

        enemyObject.GetComponent<SpriteRenderer>().sprite = GetComponent<EnemiesData>().enemies[dataID].sprite;
        MapManager.enemies.Add(enemy);

        int id = MapManager.enemies.IndexOf(enemy);

        MapManager.map[position.x, position.y].hasEnemy = true;
        MapManager.map[position.x, position.y].enemyObject = enemyObject;
        MapManager.map[position.x, position.y].enemyID = id;

        enemyObject.GetComponent<EnemyBrain>().Initialize(id, position);
    }

    string CallEnemy(out int id) {
        string name = "";

        id = -1;
        int chance = Random.Range(1, 100);

        EnemiesData data = GetComponent<EnemiesData>();

        for (int i = data.enemies.Length - 1; i >= 0; i--) {
            if (chance <= data.enemies[i].spawnChance) {
                name = data.enemies[i].name;
                id = i;
                break;
            }
        }

        return name;
    }
}
