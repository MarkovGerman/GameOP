using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] public int health = 100;
    private int power = 10;
    public float Speed;


    private void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.left * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
            collider.GetComponent<PlayerInteraction>().TakeDamage(power);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
