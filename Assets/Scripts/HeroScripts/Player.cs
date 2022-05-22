using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private Rigidbody2D rb;

    public float Speed;

    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GameObject.Find("standcharacterF").GetComponent<Animator>();
        sprite = GameObject.Find("standcharacterF").GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        var w = Input.GetKey(KeyCode.W) ? 1 : 0;
        var s = Input.GetKey(KeyCode.S) ? -1 : 0;
        var a = Input.GetKey(KeyCode.A) ? -1 : 0;
        var d = Input.GetKey(KeyCode.D) ? 1 : 0;

        MovementVector = new Vector2(a + d, w + s);

        rb.velocity = MovementVector * Speed;
        rb.mass = 10;
        rb.angularDrag = 10;

        if (rb.velocity.x >= 0) sprite.flipX = false;
        else sprite.flipX = true;

        anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            transform.position = new Vector3(-140f, 4f, -1f);
        }
    }
}