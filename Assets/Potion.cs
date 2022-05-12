using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float CountHeart;
    private Health health;
    bool IsDrunk;

    void Start(){
        IsDrunk = false;
        health = gameObject.GetComponent<Health>();
    }

    void Update()
    {
        if (IsDrunk)
            Destroy(gameObject);
    }

     void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.tag == "Player"){
            health.SelfHealth += CountHeart;
            IsDrunk = true;
         }
     }
}
