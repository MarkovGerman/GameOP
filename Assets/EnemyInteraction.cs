using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    public int health;
    public int power;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
            collider.GetComponent<PlayerInteraction>().TakeDamage(power);
    }
}
