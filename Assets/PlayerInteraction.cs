using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int health;
    public int Health => health;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            Die();
    }

    public void Die()
    {
        health = 100;
    }
}
