using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private GameObject player;

    public int FrameRate;

    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player");
        var playerPos = player.transform.position;
        playerPos.z = -10f;
        transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * FrameRate);
    }
}