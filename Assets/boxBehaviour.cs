using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxBehaviour : MonoBehaviour
{
    private float health;

    void Update()
    {
        health = GetComponent<Health>().SelfHealth;

        if (health <= 0)
            Destroy(gameObject);
    }
}
