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
    public float ControllersOffTime = 10;
    private float offTimer;


    private Vector2 MovementVector;
    private float Count;
    private bool isSlowed;
    private Rigidbody2D rigidBodyComponent;
    public float Speed;
    
    void Start()
    {
        ControllersOffTime *= Time.deltaTime;
        offTimer = 0f;
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        rigidBodyComponent.mass = 10;
    }

    void Update()
    {
        //CheckHealth();
        if (offTimer <= 0)
        {
            var w = Input.GetKey(KeyCode.W) ? 1 : 0;
            var s = Input.GetKey(KeyCode.S) ? -1 : 0;
            var a = Input.GetKey(KeyCode.A) ? -1 : 0;
            var d = Input.GetKey(KeyCode.D) ? 1 : 0;
            MovementVector = new Vector2(a + d, w + s);

            rigidBodyComponent.velocity = MovementVector * Speed;         
            rigidBodyComponent.angularDrag = 10;
        }
        else
        {
            offTimer -= Time.deltaTime;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            transform.position = new Vector3(-140f, 4f, -1f);
        }
    }

    public void Froze(float count)
    {
        if (!isSlowed)
        {
            Count = count;
            Speed = Speed / count;
        }
        isSlowed = true;
    }

    public void AntiFroze()
    {
        if (isSlowed)
        {
            Speed *= Count;
            Count = 1;
        }
        isSlowed = false;
    }

    public void SetTimer()
    {
        offTimer = ControllersOffTime;
    }
}