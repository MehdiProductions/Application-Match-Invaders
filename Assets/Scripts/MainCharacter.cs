using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed;
    Vector2 PositionPlayer;
    float limitx = 11f;
    public bool CanMove = true;

    void Start()
    {
        PositionPlayer = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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

}
