using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

    private SpriteRenderer render;
    private int maxMatch;
    private bool matchFound = false;
    Sprite sprite1;
    public Sprite Sprite2;
    public GameObject ExplosionPrefab;
    public int aliensKilled = 0;
    public GameObject bullet;
    LevelManager levelmanager;


    private void Awake()
    {

        render = GetComponent<SpriteRenderer>();
        sprite1 = render.sprite;
        maxMatch = 4;
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        InvokeRepeating("ShootBullet", Random.Range(1f, 6f), Random.Range(1f, 3f));

    }


    void AnimateAlien()
    {
        if (render.sprite)
        {
            Sprite nextSprite = render.sprite == sprite1 ? Sprite2 : sprite1;
            render.sprite = nextSprite;
        }
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
                levelmanager.Remainingalien -= 1;
                aliensKilled += 1;
                Instantiate(ExplosionPrefab, matchingAliens[i].transform.position, Quaternion.identity, transform);
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
                levelmanager.Remainingalien -= 1;
                aliensKilled += 1;
                Instantiate(ExplosionPrefab, matchingAliens[i].transform.position, Quaternion.identity, transform);
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
            levelmanager.Remainingalien -= 1;
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform);

        }

    }

    public void ShootBullet()
    {
        if (transform.tag == "Alien" + levelmanager.CurrentRow && levelmanager.bulletCount < 5 && transform.position.y > -3.8f)
        {
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.3f), Quaternion.identity);
        }
    }

    //public void StopShooting() //new
    //{
    //    CancelInvoke();
    //}

    private void OnTriggerEnter2D(Collider2D col) // new
    {
        if (col.gameObject.name == "Bottom")
        {
            GameOver();
        }
    }
    void GameOver() // new
    {
        Debug.Log("GameOver");
    }

}








