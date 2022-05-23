using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    public float Speed;
    public Vector2 side = new Vector2(1, 0);
    public float TimeBtwSide;

    private bool flipX;
    private float timer;
    private Rigidbody2D rb;

    void Start()
    {
        flipX = GetComponent<SpriteRenderer>().flipX;
        TimeBtwSide *= Time.deltaTime;
    }

    void Update()
    {
        if (timer >= TimeBtwSide)
        {
            flipX = !flipX;
            side *= -1;

            rb.velocity = side * Speed;

            timer = 0f;
        }

        timer += Time.deltaTime;
    }
}
