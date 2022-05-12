using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxin : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            //PlayAnimation();            
        }
    }

    //void PlayAnimation(){}
}
