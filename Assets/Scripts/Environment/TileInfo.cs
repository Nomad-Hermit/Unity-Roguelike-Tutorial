using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public Vector2Int position;

    public bool isVisible;
    public bool isExplored;

    SpriteRenderer sprite;

    public void Initialize(Vector2Int pos) {
        position = pos;

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.clear;
    }

    public void UpdateVisibility() {
        isVisible = MapManager.map[position.x, position.y].isVisible;
        isExplored = MapManager.map[position.x, position.y].isExplored;

        if (isVisible) sprite.color = Color.white;
        else if (isExplored) sprite.color = Color.grey;
        else sprite.color = Color.clear;
    }
}
