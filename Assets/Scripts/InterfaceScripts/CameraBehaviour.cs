using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player");
        transform.position = player.transform.position + offset/32;
    }
}