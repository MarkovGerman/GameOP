using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : MonoBehaviour
{
    public bool enterExit = false;
    public float TimeBtwMist = 10f;

    private float timer;
    private SpriteRenderer spriteRenderer;
    private bool exited;

    private void Start()
    {     
        TimeBtwMist *= Time.deltaTime;
        timer = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer = TimeBtwMist;
            exited = false;
        }      
    }

    private void FixedUpdate()
    {
        if (timer > 0f)
        {
            var curColor = spriteRenderer.color;
            spriteRenderer.color = new Color(curColor.r, curColor.g, curColor.b, curColor.a - 0.1f);
            timer -= Time.deltaTime;
        }
        else if (exited && spriteRenderer.color.a <= 1)
        {
            var curColor = spriteRenderer.color;
            spriteRenderer.color = new Color(curColor.r, curColor.g, curColor.b, curColor.a + 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enterExit == true && collision.gameObject.tag == "Player")
            exited = true;
    }

}
