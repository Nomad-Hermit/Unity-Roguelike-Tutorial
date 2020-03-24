using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;

    public int widthMinRoom;
    public int widthMaxRoom;
    public int heightMinRoom;
    public int heightMaxRoom;

    public int maxCorridorLength;
    public int maxFeatures;

    public bool isASCII;

    public void InitializeDungeon() {
        MapManager.map = new Tile[mapWidth, mapHeight];
    }

    public void GenerateDungeon() {
        FirstRoom();
        DrawMap(isASCII);
    }

    void FirstRoom() {
        Feature room = new Feature();
        room.positions = new List<Position>();

        int roomWidth = Random.Range(widthMinRoom, widthMaxRoom);
        int roomHeight = Random.Range(heightMinRoom, heightMaxRoom);

        int xStartingPoint = mapWidth / 2;
        int yStartingPoint = mapHeight / 2;

        xStartingPoint -= Random.Range(0, roomWidth);
        yStartingPoint -= Random.Range(0, roomHeight);

        room.walls = new Wall[4];

        for (int i = 0; i < room.walls.Length; i++) {
            room.walls[i] = new Wall();
            room.walls[i].positions = new List<Position>();
            room.walls[i].length = 0;

            switch (i) {
                case 0:
                    room.walls[i].direction = "South";
                    break;
                case 1:
                    room.walls[i].direction = "North";
                    break;
                case 2:
                    room.walls[i].direction = "West";
                    break;
                case 3:
                    room.walls[i].direction = "East";
                    break;
            }
        }

        for (int y = 0; y < roomHeight; y++) {
            for (int x = 0; x < roomWidth; x++) {
                Position position = new Position();
                position.x = xStartingPoint + x;
                position.y = yStartingPoint + y;

                room.positions.Add(position);

                MapManager.map[position.x, position.y] = new Tile();
                MapManager.map[position.x, position.y].xPosition = position.x;
                MapManager.map[position.x, position.y].yPosition = position.y;

                if (y == 0) {
                    room.walls[0].positions.Add(position);
                    room.walls[0].length++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                else if (y == (roomHeight - 1)) {
                    room.walls[1].positions.Add(position);
                    room.walls[1].length++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                else if (x == 0) {
                    room.walls[2].positions.Add(position);
                    room.walls[2].length++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                else if (x == (roomWidth - 1)) {
                    room.walls[3].positions.Add(position);
                    room.walls[3].length++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                else {
                    MapManager.map[position.x, position.y].type = "Floor";
                }
            }
        }

        room.width = roomWidth;
        room.height = roomHeight;
        room.type = "Room";
    }

    void DrawMap(bool isASCII) {
        if (isASCII) {
            Text screen = GameObject.Find("ASCIITest").GetComponent<Text>();

            string asciiMap = "";

            for (int y = (mapHeight - 1); y >= 0; y--) {
                for (int x = 0; x < mapWidth; x++) {
                    if (MapManager.map[x,y] != null) {
                        switch (MapManager.map[x, y].type) {
                            case "Wall":
                                asciiMap += "#";
                                break;
                            case "Floor":
                                asciiMap += ".";
                                break;
                        }
                    } else {
                        asciiMap += " ";
                    }

                    if (x == (mapWidth - 1)) {
                        asciiMap += "\n";
                    }
                }
            }

            screen.text = asciiMap;
        }
    }
}
