using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{

    [SerializeField] private float Force = 600f, DestroyTime = 1f;
    Rigidbody2D rb;
    private MainCharacter maincharacter;
    LevelManager levelmanager;
    GameManager GameManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maincharacter = GameObject.Find("Player").GetComponent<MainCharacter>();
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Force = GameManager.speedBullets;
        DestroyTime = 600 / Force;
    }


    void Start()
    {
        rb.AddForce(Vector2.up * Force);
        Destroy(gameObject, DestroyTime);
    }

    private void OnDestroy()
    {
        maincharacter.CanShoot = true;
    }
   
    private int fibonacci(int n)
    {
        int a = 0;
        int b = 1;

        for (int i = 0; i <= n; i++)
        {
            int temp = a;
            a = b;
            b = temp + b;
        }

        return a;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < GameObject.Find("LevelManager").GetComponent<LevelManager>().ySize; i++)
        {
            if (col.CompareTag("Alien" + i) && col.gameObject.GetComponent<SpriteRenderer>().sprite != null)
            {


                int aliensKilled;
                Destroy(gameObject);
                col.gameObject.GetComponent<Alien>().ClearAllMatches(); // Call the Match 3 function
                aliensKilled = col.gameObject.GetComponent<Alien>().aliensKilled + 1;
                maincharacter.Score += fibonacci(aliensKilled) * 10 * aliensKilled;  // Calculate number of points following the Fibonacci suite


            }
        }

        if (col.name == "Bunker(Clone)")
        {
            float a = col.GetComponent<SpriteRenderer>().color.a;
            if (a == 0.25f)
            {
                Destroy(col.gameObject);
            }
            col.GetComponent<SpriteRenderer>().color = new Color(0, 0.2f, 0, a -= 0.25f);
            Destroy(gameObject);
        }
    }
}
