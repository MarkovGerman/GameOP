using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionInteraction : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var keysNum = gameObject.GetComponent<Inventory>().KeysNum;

        if (collision.gameObject.tag == "Door" && keysNum > 0)
        {
            Destroy(collision.gameObject);
            gameObject.GetComponent<Inventory>().KeysNum--;
        }

        if (collision.gameObject.tag == "Key")
        {
            gameObject.GetComponent<Inventory>().KeysNum += 2;
            Destroy(collision.gameObject);
        }
    }
}
