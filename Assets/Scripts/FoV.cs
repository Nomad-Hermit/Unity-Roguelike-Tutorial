using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FoV 
{
    static List<Vector2Int> borderTiles;

    static public void Initialize() {
        borderTiles = new List<Vector2Int>();

        Vector2Int vectorToAdd;
        for (int i = -4; i <= 4; i++) {
            vectorToAdd = new Vector2Int(i, -4);
            if (!borderTiles.Contains(vectorToAdd)) borderTiles.Add(vectorToAdd);
            vectorToAdd = new Vector2Int(i, 4);
            if (!borderTiles.Contains(vectorToAdd)) borderTiles.Add(vectorToAdd);
            vectorToAdd = new Vector2Int(-4, i);
            if (!borderTiles.Contains(vectorToAdd)) borderTiles.Add(vectorToAdd);
            vectorToAdd = new Vector2Int(4, i);
            if (!borderTiles.Contains(vectorToAdd)) borderTiles.Add(vectorToAdd);
        }
    }

    static public void GetPlayerFoV(Vector2Int position) {
        for (int y = 0; y < GameObject.Find("GameManager").GetComponent<DungeonGenerator>().mapHeight; y++) {
            for (int x = 0; x < GameObject.Find("GameManager").GetComponent<DungeonGenerator>().mapWidth; x++) {
                if (MapManager.map[x, y] != null) {
                    MapManager.map[x, y].isVisible = false;
                }
            }
        }

        foreach (Vector2Int borderTile in borderTiles) {
            Debug.Log("border tile: " + borderTile);
            foreach(Vector2Int cell in GetCellsAlongLine(position, position + borderTile)) {
                Debug.Log("Cells Along Line: " + cell);
                MapManager.map[cell.x, cell.y].isVisible = true;
                MapManager.map[cell.x, cell.y].isExplored = true;
                if (MapManager.map[cell.x, cell.y].isOpaque) {
                    break;
                }
            }
        }
    }

    static public void VisibilityCheck(Vector2Int position) {
        if (MapManager.map[position.x, position.y].isVisible) {
            MapManager.map[position.x, position.y].color = "white";
        }
        else {
            if (MapManager.map[position.x, position.y].isExplored) {
                MapManager.map[position.x, position.y].color = "grey";
            } else {
                MapManager.map[position.x, position.y].color = "black";
            }
        }
        MapManager.map[position.x, position.y].baseObject.GetComponent<TileInfo>().UpdateVisibility();
    }

    static List<Vector2Int> GetCellsAlongLine(Vector2Int origin, Vector2Int destination) {
        List<Vector2Int> cells = new List<Vector2Int>();

        int mapWidth = GameObject.Find("GameManager").GetComponent<DungeonGenerator>().mapWidth;
        int mapHeight = GameObject.Find("GameManager").GetComponent<DungeonGenerator>().mapHeight;
        Vector2Int maxPosition = new Vector2Int(mapWidth - 1, mapHeight - 1);
        origin.Clamp(Vector2Int.zero, maxPosition);
        destination.Clamp(Vector2Int.zero, maxPosition);

        Vector2Int delta = new Vector2Int(
            Math.Abs(destination.x - origin.x),
            Math.Abs(destination.y - origin.y)
        );
        Vector2Int step = new Vector2Int(
            origin.x < destination.x ? 1 : -1,
            origin.y < destination.y ? 1 : -1
        );
        int err = delta.x - delta.y;

        while (true) {
            cells.Add(origin);
            if (origin.x < 0 || origin.y < 0 || origin.x > mapWidth || origin.y > mapHeight)
                break;
            if (MapManager.map[origin.x, origin.y] == null)
                break;
            if (origin == destination)
                break;

            int e2 = err * 2;
            if (e2 > -delta.y) {
                err -= delta.y;
                origin.x += step.x;
            }
            if (e2 < delta.x) {
                err += delta.x;
                origin.y += step.y;
            }
        }

        return cells;
    }
}
