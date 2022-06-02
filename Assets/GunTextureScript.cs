using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunTextureScript : MonoBehaviour
{
    public GameObject gun;
    private GameObject player;
    private bool isNear;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && isNear)
        {
            Destroy(GameObject.FindGameObjectWithTag("Gun"));
            var obj = Instantiate(gun, player.transform);
            obj.transform.SetParent(player.transform);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isNear = true;
            GameObject.Find("MessageBox").GetComponent<Text>().text = "Press E to pick weapon!";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isNear = false;
        GameObject.Find("MessageBox").GetComponent<Text>().text = "";
    }
}
