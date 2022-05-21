using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public bool PlayerInArea;
    private Transform player;

    private string detTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detTag))
        {
            PlayerInArea = true;
            player = collision.gameObject.transform;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detTag))
        {
            PlayerInArea = false;
            player = null;
        }
    }
}
