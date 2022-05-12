using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private Health healthPlayer;
    void Start(){
        healthPlayer = GetComponent<Health>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healthPlayer.SelfHealth -= 1.0f;
        }
    }
}
