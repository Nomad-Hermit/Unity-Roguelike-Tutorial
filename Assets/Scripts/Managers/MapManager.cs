using System.Collections;
using System; // So the script can use the serialization commands
using System.Collections.Generic;
using UnityEngine;

public class MapManager {
    public static Tile[,] map; // the 2-dimensional map with the information for all the tiles
}

[Serializable] // Makes the class serializable so it can be saved out to a file
public class Tile { // Holds all the information for each tile on the map
    public int xPosition; // the position on the x axis
    public int yPosition; // the position on the y axis
    [NonSerialized]
    public GameObject baseObject; // the map game object attached to that position: a floor, a wall, etc.
    public string type; // The type of the tile, if it is wall, floor, etc
}

[Serializable]
public class Position { //A class that saves the position of any cell
    public int x;
    public int y;
}

[Serializable]
public class Wall { // A class for saving the wall information, for the dungeon generation algorithm
    public List<Position> positions;
    public string direction;
    public int length;
    public bool hasFeature = false;
}

[Serializable]
public class Feature { // A class for saving the feature (corridor or room) information, for the dungeon generation algorithm
    public List<Position> positions;
    public Wall[] walls;
    public string type;
    public int width;
    public int height;
}
