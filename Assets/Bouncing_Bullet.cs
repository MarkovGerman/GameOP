using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing_Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2;

    // Update is called once per frame
    void Start(){
        rb2 = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Walls")
        {
            rb2.transform.position = Vector3.Reflect(rb2.transform.position, Vector3.right);
        }
    }
}
