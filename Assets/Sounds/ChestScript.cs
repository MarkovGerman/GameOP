using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    public Sprite Opened;
    public GameObject Item;
    public bool CanBeOpened = false;
    public int PointsToOpen;

    private bool opened = false;
    private int toOpen = 0;

    private void FixedUpdate()
    {
        if (toOpen == 1 && Input.GetKey(KeyCode.E) && CanBeOpened && !opened)
        {
            opened = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = Opened;
            if (Item != null) Instantiate(Item);
        }

        if (GameObject.Find("Player").GetComponent<ScoreManagement>().Score >= PointsToOpen)
            CanBeOpened = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && CanBeOpened)
        {
            if (!opened)GameObject.Find("MessageBox").GetComponent<Text>().text = "Press E to open chest!";
            toOpen = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject.Find("MessageBox").GetComponent<Text>().text = "";
        toOpen = 0;
    }
}