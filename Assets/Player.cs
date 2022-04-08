using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions
{
    public static float GetAngle(this Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}

public class Player : MonoBehaviour
{
    private Vector2 MovementVector;
    private Rigidbody2D rigidBodyComponent;
    private Vector2 acceleration = new Vector2(10, 10);

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        var w = Input.GetKey(KeyCode.W) ? 1 : 0;
        var s = Input.GetKey(KeyCode.S) ? -1 : 0;
        var a = Input.GetKey(KeyCode.A) ? -1 : 0;
        var d = Input.GetKey(KeyCode.D) ? 1 : 0;

        //var f = Input.GetKey();

        MovementVector = new Vector2(a + d, w + s);

        rigidBodyComponent.velocity = MovementVector * acceleration;
        rigidBodyComponent.mass = 10;
        rigidBodyComponent.angularDrag = 10;

        if (MovementVector.magnitude > Mathf.Epsilon)
        {
            var angle = new Vector3(0, 0, MovementVector.GetAngle());
            transform.rotation = Quaternion.Euler(angle);
        }
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Square")
    //     {
    //         Debug.Log("Hit smth");
    //     }
    // }
}