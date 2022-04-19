using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject[] boxes;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        boxes = GameObject.FindGameObjectsWithTag("Box");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");

    }
}
