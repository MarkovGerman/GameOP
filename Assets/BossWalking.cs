using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Right = 0, 
    Left = 1, 
    Left2 =2, 
    Right2 = 3, 
    Up = 4, 
    Down = 5, 
    Down2 = 6, 
    Up2 = 7
};

public class BossWalking : MonoBehaviour
{
    private Rigidbody2D rb2;
    private GameObject player;
    public float Speed;
    private float curtime;
    public float time;
    int direction;
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        curtime = 0f;
        direction = (int) Direction.Up;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if ((player.transform.position - transform.position).magnitude <= 50)
            StartWalking();
    }

    void StartWalking()
    {
        curtime += Time.deltaTime;
        if (curtime < time)
        {
            Move();
        }
        else
        {
            curtime = 0f;
            direction = (direction + 1) % 8;
        }
    }

    void Move()
    {
        if (direction == 0 || direction == 3)
            MoveRight();
        if (direction == 1 || direction == 2)
            MoveLeft();
        if (direction == 4 || direction == 7)
            MoveUp();
        if (direction == 5 || direction == 6)
            MoveDown();  
    }
    void MoveLeft()
    {
        var MovementVector = new Vector2(-1, 0);
        rb2.velocity = MovementVector * Speed;
    }

    void MoveRight()
    {
        var MovementVector = new Vector2(1, 0);
        rb2.velocity = MovementVector * Speed;   
    }
    void MoveUp()
    {
        var MovementVector = new Vector2(0, 1);
        rb2.velocity = MovementVector * Speed; 
    }
    void MoveDown()
    {
        var MovementVector = new Vector2(0, -1);
        rb2.velocity = MovementVector * Speed; 
    }
}
