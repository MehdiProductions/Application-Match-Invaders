using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAlien : MonoBehaviour
{
    [SerializeField] private float DestroyTime = 1f;
    LevelManager levelmanager;
    

    void Start()
    {
        
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelmanager.bulletCount += 1;
        GameObject BulletsAlien = GameObject.Find("BulletsAlien");
        transform.parent = BulletsAlien.transform;

    }


    void Update()
    {
        if (transform.position.y < -3.8f) GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Limits"))
        {
            Destroy(gameObject);
            levelmanager.bulletCount -= 1;

        }

        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
            levelmanager.bulletCount -= 1;
        }

        if (col.name == "Bunker(Clone)")
        {
            levelmanager.bulletCount -= 1;
            float a = col.GetComponent<SpriteRenderer>().color.a;
            if (a == 0.25f)
            {
                Destroy(col.gameObject);
            }
            col.GetComponent<SpriteRenderer>().color = new Color(0, 0.2f, 0, a -= 0.25f); // Lower transparency of the bunkers at each hit
            Destroy(gameObject);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}