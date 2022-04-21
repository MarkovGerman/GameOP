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
    private Rigidbody2D rigidBodyComponent;
    public float Speed;
    public float Health;

    public int NumOfHearts;
    public Image[] Hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public float Heal;

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        CheckHealth();

        var w = Input.GetKey(KeyCode.W) ? 1 : 0;
        var s = Input.GetKey(KeyCode.S) ? -1 : 0;
        var a = Input.GetKey(KeyCode.A) ? -1 : 0;
        var d = Input.GetKey(KeyCode.D) ? 1 : 0;


        MovementVector = new Vector2(a + d, w + s);

        rigidBodyComponent.velocity = MovementVector * Speed;
        rigidBodyComponent.mass = 10;
        rigidBodyComponent.angularDrag = 10;
    }

    void CheckHealth()
    {
        if (Health > NumOfHearts)
        {
            Health = NumOfHearts;
        }

        Health += Time.deltaTime * Heal;

        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(Health))
            {
                Hearts[i].sprite = FullHeart;
            }
            else
            {
                Hearts[i].sprite = EmptyHeart;
            }

            if (i < NumOfHearts)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }

            if (Health < 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            transform.position = new Vector3(-140f, 4f, -1f);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}