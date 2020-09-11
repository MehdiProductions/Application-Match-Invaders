using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Settings", menuName = "Settings")]
public class Settings : ScriptableObject
{

    public float speedEnemies;
    public float speedPlayer;
    public float speedBullets;
    public int lives;
    public int xSize, ySize;

}
