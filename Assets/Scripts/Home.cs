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
        SceneManager.LoadScene("MainScene");
    }
}
