using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MainCharacter : MonoBehaviour
{

    [SerializeField] private float speed;
    
    private GameObject NewHighScore;
    private GameObject NewHighScore500;
    private int score = 0;

    public GameObject BulletPrefab;
    public int Highscore;
    public bool CanShoot = true;
    public bool CanMove = true;

    bool detect = true;
    bool almostHS = true;
    float limitx = 11f;

    Vector2 PositionPlayer;
    Transform Canon;
    Text txtScore;
    Text txthighScore; 
    LevelManager Wave;
    Lives lives;    
    CameraShake cameraShake;
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
                ImageTemperature();

            }
            if (Score > PlayerPrefs.GetInt("HighScore") - 500 && almostHS && PlayerPrefs.GetInt("HighScore") > 500)
            {

                NewHighScore500.GetComponent<Text>().text = "ONLY " + (PlayerPrefs.GetInt("HighScore") - Score) + " FOR HIGSHCORE";
                StartCoroutine(newHighscored500());
                almostHS = false;
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
        NewHighScore500 = GameObject.Find("NewHighScore500");
        NewHighScore.GetComponent<Text>().color = new Color(0, 1, 0, 0);
        NewHighScore500.GetComponent<Text>().color = new Color(1, 0, 0, 0);
        NewHighScore500.GetComponentInChildren<MeshRenderer>().enabled = false;
        Score = 0;
        txtScore.text = "Score: " + score;
        InitialImageTemperature();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        InitialVignette();
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
        StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
        Vignette();
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
            NewHighScore.GetComponent<Text>().color = new Color(0,1,0,1);
            yield return new WaitForSeconds(1f);
            NewHighScore.GetComponent<Text>().color = new Color(0, 1, 0, 0);
        }
    }

    IEnumerator newHighscored500()
    {
        
            NewHighScore500.GetComponent<Text>().color = new Color(1, 0, 0, 1);
            NewHighScore500.GetComponentInChildren<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(3f);
            NewHighScore500.GetComponent<Text>().color = new Color(1, 0, 0, 0);
            NewHighScore500.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    void ImageTemperature()
    {
        PostProcessVolume volume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
        ColorGrading colorGradingLayer = null;
        volume.profile.TryGetSettings(out colorGradingLayer);
        colorGradingLayer.temperature.value = 70;
    }

    void InitialImageTemperature()
    {
        PostProcessVolume volume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
        ColorGrading colorGradingLayer = null;
        volume.profile.TryGetSettings(out colorGradingLayer);
        colorGradingLayer.temperature.value = -70;
    }

    void Vignette()
    {
        PostProcessVolume volume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
        Vignette vignette = null;
        volume.profile.TryGetSettings(out vignette);
        vignette.intensity.value += 0.14f;
    }

    void InitialVignette()
    {
        PostProcessVolume volume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
        Vignette vignette = null;
        volume.profile.TryGetSettings(out vignette);
        vignette.intensity.value = 0;
    }
}
