using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Lives : MonoBehaviour
{

    //Health manager
    private int iniHealth;
    private List<Image> hearts = new List<Image>();

    public Image[] heartprefab;
    public int health = 3;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image heart;
    public float yOffset = 1;
    public float xOffset = 32;

    GameManager GameManager;

    private void Awake()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

    }

    private void Start()
    {
        health = GameManager.lives;
        SetHealth();
    }
    public void SetHealth()
    {
        Image[] heartprefab = new Image[health];
        health = Mathf.Clamp(health, 1, 6);
        heartprefab[health - 1] = heart;
        iniHealth = health;

        for (int i = 0; i < health - 1; i++)
        {
            
            heartprefab[i] = Instantiate(heart, new Vector2(175 + transform.position.x + xOffset * i, transform.position.y + yOffset), Quaternion.identity);
            heartprefab[i].transform.localScale *= 2.5f;
            heartprefab[i].transform.parent = transform;
            hearts.Add(heartprefab[i]);

        }
    }

    public void LooseLife()
    {

        health -= 1;
        for (int i = 0; i < hearts.Count; i++)
        {

            if (i < health - 1)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
        if (health == 0)
        {
            heart.sprite = emptyHeart;
            GameOver();

        }

    }

    public void ResetLives()
    {
        health = iniHealth;
        heart.sprite = fullHeart;
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = fullHeart;
            
        }

        if (health < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    void GameOver()
    {
        Debug.Log("GAMEOVER");
        GameObject.Find("LevelManager").GetComponent<LevelManager>().StopWave();
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Find("GameManager").GetComponent<GameManager>().Endgame();
    }

}
