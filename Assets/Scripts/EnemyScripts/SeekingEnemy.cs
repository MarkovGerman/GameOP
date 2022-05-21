using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingEnemy : MonoBehaviour
{
    public int Speed;
    public GameObject TriggerArea;

    private Rigidbody2D rigidBodyComponent;
    private Vector3 startPos;

    private void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        Speed *= 2;
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        if (TriggerArea.GetComponent<AreaDetector>().PlayerInArea)
        {
            var player = GameObject.Find("Player");

            var direction = (player.transform.position - transform.position).normalized;

            rigidBodyComponent.velocity = direction * Speed;
        }

        else
        {
            if ((startPos - transform.position).magnitude >= 1.0f)
            {
                var direction = (startPos - transform.position).normalized;
                rigidBodyComponent.velocity = direction * Speed;
            }

            else
            {
                rigidBodyComponent.velocity = Vector3.zero;
            }
        }
    }
}
