using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    private int flag = 0;
    private Animation anim;

    private bool opened = false;

    private void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        anim.Stop();
    }

    private void Update()
    {
        if (flag == 1)
        {
            if (GameObject.Find("Player").GetComponent<Inventory>().KeysNum > 0)
            {
                GameObject.Find("Player").GetComponent<Inventory>().KeysNum--;
                GameObject.Find("MessageBox").GetComponent<Text>().text = "";
                anim.Play();
                opened = true;
            }

            else
                GameObject.Find("MessageBox").GetComponent<Text>().text = "Ops, you haven't got any keys (";
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (opened)
                anim.Play();
            else
            {
                GameObject.Find("MessageBox").GetComponent<Text>().text = "Press E to open the Door";
                flag = 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            flag = 0;
            GameObject.Find("MessageBox").GetComponent<Text>().text = "";
        }
    }
}
