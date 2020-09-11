using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> alientype = new List<GameObject>();    
    [SerializeField] private float WaveStepRight = 1f, WaveStepDown = 1f, WaveSpeed = 0.8f;
    [SerializeField] private float initialSpeed = 0;
    public int xSize, ySize;
    public AudioClip Clipaudio;
    public int CurrentRow;

    AudioSource audiosource;

    private GameObject[,] aliens;
    GameManager GameManager;

    public bool CanMove = true;
    public bool Walkright = true;
    public int Totalalien;
    public int Remainingalien;
    public int bulletCount;

    Vector2 PositionInitialWave;
    MainCharacter maincharacter;

    public GameObject bunker; 

    private void Awake()
    {
        transform.name = "LevelManager";
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void Start()
    {

        GameObject.Find("TxtLives").GetComponent<Lives>().ResetLives();
        xSize = GameManager.xSize;
        ySize = GameManager.ySize;
        initialSpeed = GameManager.speedEnemies;
        xSize = Mathf.Clamp(xSize, 1, 17);
        ySize = Mathf.Clamp(ySize, 1, 10);
        audiosource = GetComponent<AudioSource>();
        Vector2 offset = alientype[0].GetComponent<SpriteRenderer>().bounds.size;
        GenerateGrid(offset.x + 0.4f, offset.y + 0.4f);
        StartCoroutine(MoveWave());
        PositionInitialWave = transform.position;
        maincharacter = GameObject.Find("Player").GetComponent<MainCharacter>();
        CurrentRow = 0;
        bulletCount = 0;
        bulletCount = Mathf.Clamp(bulletCount, 0, 5);
    }

    private void GenerateGrid(float xOffset, float yOffset)
    {
        aliens = new GameObject[xSize, ySize];

        float startX = transform.position.x;
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject alien = Instantiate(alientype[Random.Range(0, alientype.Count)], new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), Quaternion.identity, transform);
                alien.transform.tag = "Alien" + y;

                aliens[x, y] = alien;
               
                Totalalien = transform.childCount;
                Remainingalien = Totalalien;
            }
        }

        for (int i = 0; i < 4; i++) 
        {
            Instantiate(bunker, new Vector2(transform.position.x + 1 + 5.5f * i, transform.position.y - 4.5f), Quaternion.identity, GameObject.Find("Bunkers").transform);
        }

    }

    IEnumerator MoveWave()
    {
        while (CanMove)
        {
            Vector2 direction = Walkright ? Vector2.right : Vector2.left;
            transform.Translate(direction * WaveStepRight);
            BroadcastMessage("AnimateAlien");
            PlayWaveSound();
            NoAlienRemaining();
            CheckRow();
            SpeedWave();
            yield return new WaitForSeconds(WaveSpeed);

        }
    }

    public void WaveReachesLimit()
    {
        Walkright = !Walkright;
        transform.Translate(Vector2.down * WaveStepDown);
    }

    void PlayWaveSound()
    {
        audiosource.PlayOneShot(Clipaudio);
    }

    void NoAlienRemaining()
    {
        if (Remainingalien == 0)
        {
            Debug.Log("WIN");
            StopWave(); 
        }
    }

    void CheckRow()
    {
        for (int i = 0; i <= ySize - 1; i++)
            if (GameObject.FindGameObjectWithTag("Alien" + i) == null)
            {

                if (i == CurrentRow)
                {
                    CurrentRow = i + 1;
                }
            }

    }

    public void StopWave()
    {
        StopAllCoroutines();
        
    }

    public void RestartWave(float delay)
    {
        StartCoroutine(Restart(delay));
    }

    void SpeedWave()
    {
        if (WaveSpeed >= 0.2f)
        {
            WaveSpeed = (Remainingalien - initialSpeed) / 100f;
        }
        else
        {
            WaveSpeed = 0.2f;
        }
    }

    IEnumerator Restart(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = PositionInitialWave;
        StartCoroutine(MoveWave());
        maincharacter.InitPlayer();
    }

    public void ClearScene()
    {
        
            if (GameObject.Find("Bunkers").transform.childCount != 0)
            {
                GameObject.Find("Bunkers").BroadcastMessage("Delete");
            }
            if (GameObject.Find("BulletsAlien").transform.childCount != 0)
            {
                GameObject.Find("BulletsAlien").BroadcastMessage("Delete");
            }
        
    }

}