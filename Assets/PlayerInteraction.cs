using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int Health;
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("Die");
    }
}
