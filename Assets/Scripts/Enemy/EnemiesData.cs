using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesData : MonoBehaviour
{
    public Enemies[] enemies;
}

[System.Serializable]
public class Enemies {
    public string name;
    public Sprite sprite;
    public int spawnChance;
}
