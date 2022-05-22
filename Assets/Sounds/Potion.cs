using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float CountHeart;
    bool IsDrunk;

    void Start(){
        IsDrunk = false;
    }

    void Update()
    {
        if (IsDrunk)
            Destroy(gameObject);
    }

     void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<Health>().SelfHealth += CountHeart;
            IsDrunk = true;
         }
     }
}
