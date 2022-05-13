using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] public int health = 3;
    private int power = 10;
    public float Speed;
    public GameObject Key;
    public GameObject Heal;

    private ScoreManagement sm;

    //private ScoreManagement sm;

    private void Start()
    {
        sm = FindObjectOfType<ScoreManagement>();
    }

    private void Update()
    {

        if (health <= 0)
        {
            sm.Add();
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 2)
                Instantiate(Key, transform.position, transform.rotation);
            else
                Instantiate(Heal, transform.position, transform.rotation);
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
        Debug.Log("Take hit");
    }
}
