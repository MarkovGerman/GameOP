using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backMovements : MonoBehaviour
{
    public float finishX = 5.5f;
    public float startX = 84f;

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
            transform.position = new Vector3(startX, pos.y, pos.z);
    }
}
