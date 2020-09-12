using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public Text txthighScore;

    void Start()
    {
        txthighScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore");
    }
    public void LoadMainScene()
    {
        StartCoroutine(WaitToLoad());
    }
    

    IEnumerator WaitToLoad()
    {
        GameObject.Find("Menu").SetActive(false);
        GameObject.Find("We come in peace").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Space Invaders").GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }
}
