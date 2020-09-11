using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{

    [SerializeField] private float speed;
    public GameObject BulletPrefab;
    private GameObject NewHighScore;
    public bool CanShoot = true;
    public bool CanMove = true;
    Vector2 PositionPlayer;
    Transform Canon;
    float limitx = 11f;
    private int score = 0;
    Text txtScore;
    Text txthighScore; 
    LevelManager Wave;
    Lives lives;
    bool detect = true;
    
    public int Highscore;
    GameManager GameManager;

    public int Score

    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            txtScore.text = "Score: " + score;
            if (Score > PlayerPrefs.GetInt("HighScore")) 
            {
               
                Highscore = Score;
                txthighScore.text = "HS: " + Highscore;
                PlayerPrefs.SetInt("HighScore", Highscore);
                StartCoroutine(newHighscored());
            }

            
        }
    }
    private void Awake()
    {
        transform.name = "Player";
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void Start()
    {
        speed = GameManager.speedPlayer;
        speed = Mathf.Clamp(speed, 1, 30);
        PositionPlayer = transform.position;
        Canon = transform.Find("Canon");
        txtScore = GameObject.Find("TxtScore").GetComponent<Text>();
        Wave = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        lives = GameObject.Find("TxtLives").GetComponent<Lives>();
        txthighScore = GameObject.Find("TxtHighscore").GetComponent<Text>(); 
        txthighScore.text = "HS: " + PlayerPrefs.GetInt("HighScore").ToString();
        NewHighScore = GameObject.Find("NewHighScore");
        NewHighScore.SetActive(false);

    }


    void Update()
    {
        MovePlayer();
        PlayerShoot();
    }

    void MovePlayer()
    {
        if (CanMove == true)
        {
            PositionPlayer.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            PositionPlayer.x = Mathf.Clamp(PositionPlayer.x, -limitx, limitx);
            transform.position = PositionPlayer;
        }
    }

    void PlayerShoot()
    {

        if (Input.GetKeyDown(KeyCode.Space) && CanShoot)
        {
            CanShoot = false;
            Instantiate(BulletPrefab, Canon.position, Quaternion.identity);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < Wave.ySize; i++)
        {
            if (col.CompareTag("Alien" + i) && detect && col.GetComponent<SpriteRenderer>().sprite)
            {
                detect = false;
                StartCoroutine(Aliencollision());
                PlayerExplosion();
            }
        }
        if (col.CompareTag("BulletAlien"))
        {
            PlayerExplosionBullet();
        }

    }

    IEnumerator Aliencollision()
    {
        Wave.StopWave();
        yield return new WaitForSeconds(0.2f);
        detect = true;
    }

    void PlayerExplosion()
    {
        GetComponent<Animator>().SetTrigger("Explosion");
        GetComponent<AudioSource>().Play();
        Wave.RestartWave(1f);
        CanShoot = false;
        CanMove = false;
        lives.LooseLife();
    }

    void PlayerExplosionBullet()
    {
        GetComponent<Animator>().SetTrigger("Explosion");
        GetComponent<AudioSource>().Play();
        lives.LooseLife();
        CanShoot = false;
        CanMove = false;
        Invoke("InitPlayer", 1f);
    }

    public void InitPlayer()
    {
        GetComponent<Animator>().SetTrigger("Regular");
        CanShoot = true;
        CanMove = true;

    }

    IEnumerator newHighscored()
    {
        if (Score > 300)
        {
            NewHighScore.SetActive(true);
            yield return new WaitForSeconds(1f);
            NewHighScore.SetActive(false);
        }
    }
}
