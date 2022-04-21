using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidWalking : MonoBehaviour
{
    private Vector2 MovementVector;
    private Rigidbody2D rigidBodyComponent;
    private Vector2 acceleration = new Vector2(1, 1);
    private float direction;

    void Start()
    {
        direction = 1;
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementVector = new Vector2( 4, 0);
       // rigidBodyComponent.velocity = direction * MovementVector;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            direction *= -1;
        }
    }
}
