using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    public Sprite Opened;
    public GameObject Item;

    private int toOpen = 0;

    void FixedUpdate()
    {
        if (toOpen == 1 && Input.GetKeyDown(KeyCode.E))
        {

            gameObject.GetComponent<SpriteRenderer>().sprite = Opened;
            if (Item != null) Instantiate(Item);
            toOpen = 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && toOpen != 2)
        {
            GameObject.Find("MessageBox").GetComponent<Text>().text = "Press E to open chest!";
            toOpen = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject.Find("MessageBox").GetComponent<Text>().text = "";
        toOpen = 0;
    }
}
