using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    private int maxMatch;
    private bool matchFound = false;
    public int aliensKilled = 0;
    private SpriteRenderer render;


    private void Awake()
    {
        maxMatch = 4;
        render = GetComponent<SpriteRenderer>();
    }


    private List<GameObject> FindMatch(Vector2 castDir)
    {
        List<GameObject> matchingAliens = new List<GameObject>();
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, castDir);
        while (hit[1].collider != null && hit[1].collider.name == transform.name)
        {
            matchingAliens.Add(hit[1].collider.gameObject);
            hit = Physics2D.RaycastAll(hit[1].collider.transform.position, castDir);

        }
        return matchingAliens;
    }

    private void ClearMatch(Vector2[] paths)
    {
        List<GameObject> matchingAliens = new List<GameObject>();
        for (int i = 0; i < paths.Length; i++)
        {
            matchingAliens.AddRange(FindMatch(paths[i]));
        }

        if (matchingAliens.Count <= maxMatch)
        {

            for (int i = 0; i < matchingAliens.Count; i++)
            {
                matchingAliens[i].GetComponent<SpriteRenderer>().sprite = null;
                matchingAliens[i].transform.name = "DEAD";
                matchingAliens[i].transform.tag = "Deadalien";
                
                aliensKilled += 1;
                
            }
            matchFound = true;
        }
        else
        {

            for (int i = 0; i < maxMatch; i++)
            {
                matchingAliens[i].GetComponent<SpriteRenderer>().sprite = null;
                matchingAliens[i].transform.name = "DEAD";
                matchingAliens[i].transform.tag = "Deadalien";
                
                aliensKilled += 1;
                
            }
            matchFound = true;
        }

        maxMatch -= matchingAliens.Count;
    }

    public void ClearAllMatches()
    {
        if (render.sprite == null)
            return;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        if (matchFound)
        {
            render.sprite = null;
            transform.name = "DEAD";
            transform.tag = "Deadalien";
            matchFound = false;
            
        }

    }
}
