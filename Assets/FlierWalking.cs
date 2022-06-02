using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlierWalking : MonoBehaviour
{
    private Rigidbody2D rb2;
    public Vector2 MovementVector;
    public float TimeWalk;
    public float Pause;
    public float Speed;
    private float curtime = 0f;


    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        Pause *= Time.deltaTime;
    }

    void Update()
    {
        if (curtime < TimeWalk)
            rb2.velocity = MovementVector * Speed;
        
        if (curtime >= TimeWalk)
            rb2.velocity = new Vector2(0, 0); 
        
        if (curtime >= TimeWalk + Pause)
        {
            curtime = 0f;
            MovementVector *= -1;
        }
        curtime += Time.deltaTime;   
    }
}
