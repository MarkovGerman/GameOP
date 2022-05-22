using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    private int flag = 0;
    private Animation anim;

    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorClose;
    private bool opened = false;

    private void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        anim.Stop();
    }

    private void FixedUpdate()
    {
        if (flag == 1 && Input.GetKey(KeyCode.E) && !opened)
        {
            if (GameObject.Find("Player").GetComponent<Inventory>().KeysNum > 0)
            {
                GameObject.Find("Player").GetComponent<Inventory>().KeysNum--;
                GameObject.Find("MessageBox").GetComponent<Text>().text = "";
                anim.Play();
                doorOpen.Play();
                opened = true;
            }
            else if (!opened)
                GameObject.Find("MessageBox").GetComponent<Text>().text = "Ops, you haven't got any keys (";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (opened)
        {
            doorOpen.Play();
            anim.Play();
        }
        else if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("MessageBox").GetComponent<Text>().text = "Press E to open the Door";
            flag = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            flag = 0;
            GameObject.Find("MessageBox").GetComponent<Text>().text = "";
            doorClose.Play();
        }
    }
}
