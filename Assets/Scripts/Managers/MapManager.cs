using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager 
{
    public static Tile[,] map; // the 2-dimensional map with the information for all the tiles
}

public class Tile { //Holds all the information for each tile on the map
    public int xPosition; // the position on the x axis
    public int yPosition; // the position on the y axis
    public GameObject baseObject; // the map game object attached to that position: a floor, a wall, etc.
}
