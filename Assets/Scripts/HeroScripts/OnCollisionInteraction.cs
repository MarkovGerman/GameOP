using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            collision.gameObject.GetComponent<Animation>().Play();
            gameObject.GetComponent<Inventory>().KeysNum++;
            Destroy(collision.gameObject);
        }
    }
}
