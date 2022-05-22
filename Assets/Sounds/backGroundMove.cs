using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundMove : MonoBehaviour
{
    public float yPos = -28;
    public float finishX = -190f;
    public float startX = 204f;

    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector3(-1, 0);
    }

    void Update()
    {
        var pos = transform.position;
        if (pos.x <= finishX)
            transform.position = new Vector3(startX, yPos, pos.z);
    }
}
