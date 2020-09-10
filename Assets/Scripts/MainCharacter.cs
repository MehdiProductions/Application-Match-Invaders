using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{

    [SerializeField] private float speed;
    public GameObject BulletPrefab;
    public bool CanShoot = true;
    public bool CanMove = true;
    Vector2 PositionPlayer;
    Transform Canon;
    float limitx = 11f;
    private int score = 0;
 

    LevelManager Wave;
    
    bool detect = true;
    
    private void Awake()
{
    transform.name = "Player";
}

void Start()
{
    PositionPlayer = transform.position;
    Canon = transform.Find("Canon");

    Wave = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    
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
    
        GetComponent<AudioSource>().Play();
    Wave.RestartWave(1f);
    CanShoot = false;
    CanMove = false;
    
    }

void PlayerExplosionBullet()
{
    
        GetComponent<AudioSource>().Play();
    
    CanShoot = false;
    CanMove = false;
    Invoke("InitPlayer", 1f);
}

public void InitPlayer()
{
    
    CanShoot = true;
    CanMove = true;

}
}
