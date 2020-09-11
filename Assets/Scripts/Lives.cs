﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{

    [SerializeField] private int health = 3;
    private int iniHealth;
    public Image[] heartprefab;
    private List<Image> hearts = new List<Image>();
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image heart;
    public float yOffset = 1;
    public float xOffset = 32;

    private void Awake()
    {

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

            heartprefab[i] = Instantiate(heart, new Vector2(72 + transform.position.x + xOffset * i, transform.position.y + yOffset), Quaternion.identity);
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