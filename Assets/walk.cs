using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    public float Speed;
    public Vector2 side = new Vector2(1, 0);
    public float TimeBtwSide;
    private SpriteRenderer renderer;

    private float timer;
    private Rigidbody2D rb;

    void Start()
    {
        TimeBtwSide *= Time.deltaTime;
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.flipX = !renderer.flipX;
    }

    void Update()
    {
        if (timer >= TimeBtwSide)
        {
            renderer.flipX = !renderer.flipX;
            side *= -1;

            rb.velocity = side * Speed;

            timer = 0f;
        }

        timer += Time.deltaTime;
    }
}
