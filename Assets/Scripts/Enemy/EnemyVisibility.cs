using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    SpriteRenderer sprite;

    public void Initialize() {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void VisibilityCheck(Vector2Int position) {
        if (MapManager.map[position.x, position.y].isVisible) {
            sprite.color = Color.white;
        }
        else {
            sprite.color = Color.clear;
        }
    }
}
