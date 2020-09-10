using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>


public class LevelManager : MonoBehaviour
{

    public List<GameObject> alientype = new List<GameObject>();
    public int xSize, ySize;
    private GameObject[,] aliens;

    void Start()
    {
        xSize = Mathf.Clamp(xSize, 1, 17);
        ySize = Mathf.Clamp(ySize, 1, 10);
        Vector2 offset = alientype[0].GetComponent<SpriteRenderer>().bounds.size;
        GenerateGrid(offset.x + 0.4f, offset.y + 0.4f);
    }

    private void GenerateGrid(float xOffset, float yOffset)
    {
        aliens = new GameObject[xSize, ySize];

        float startX = transform.position.x;
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject alien = Instantiate(alientype[Random.Range(0, alientype.Count)], new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), Quaternion.identity, transform);
                alien.transform.tag = "Alien" + y;

                aliens[x, y] = alien;
                
            }
        }
    }


}
