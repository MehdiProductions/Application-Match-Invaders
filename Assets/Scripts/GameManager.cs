using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Whole Game Logic and Settings HUB
    [SerializeField] private GameObject LevelManager;
    [SerializeField] private GameObject Player;

    private bool GameIsPaused = false;

    public float speedEnemies;
    public float speedPlayer;
    public float speedBullets;
    public int lives;
    public int xSize, ySize;
    public Settings settings;    
    public GameObject pauseMenuUI;
    public GameObject endMenuUI;
    public Text txtScore;
    public Text txthighScore;
     

    private void Awake()
    {
        Time.timeScale = 1;
        speedEnemies = settings.speedEnemies;
        speedPlayer = settings.speedPlayer;
        speedBullets = settings.speedBullets;
        lives = settings.lives;
        xSize = settings.xSize;
        ySize = settings.ySize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Endgame()
    {
        endMenuUI.SetActive(true);        
        Time.timeScale = 0f;
        txtScore.text = "LAST SCORE: " + GameObject.Find("Player").GetComponent<MainCharacter>().Score;
        txthighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore");
        if (GameObject.Find("Player").GetComponent<MainCharacter>().Score == PlayerPrefs.GetInt("HighScore"))
        {

            GameObject.Find("NewHighScoreEndMenu").GetComponent<Text>().color = new Color(0, 1, 0, 1);
        }
        else
        {

            GameObject.Find("NewHighScoreEndMenu").GetComponent<Text>().color = new Color(0, 1, 0, 0);
        }

        GameObject.Find("LevelManager").GetComponent<LevelManager>().ClearScene();
        Destroy(GameObject.Find("LevelManager"));
        Destroy(GameObject.Find("Player"));
        
        

    }
    public void RestartGame()
    {
        
        Instantiate(LevelManager, new Vector3(-9, 1, 0), Quaternion.identity);
        Instantiate(Player, new Vector3(0, -4.7f, 0), Quaternion.identity);
        endMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("Home");
    }

    
}
