using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] public int health = 100;
    private int power = 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
            collider.GetComponent<PlayerInteraction>().TakeDamage(power);
    }
}
