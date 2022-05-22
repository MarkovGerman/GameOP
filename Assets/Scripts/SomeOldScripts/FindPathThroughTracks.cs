using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindPathThroughTracks : MonoBehaviour
{
    public float TimeBtwSteps;
    public float Speed;
    public LayerMask Layer;
    public float TriggerRadius;

    private float curTime;
    private Rigidbody2D rigidBody;

    void Start()
    {
        TimeBtwSteps *= Time.deltaTime;
        curTime = 0f;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime >= TimeBtwSteps)
        {
            var playerPos = Physics2D.OverlapCircle(transform.position, TriggerRadius * 2, LayerMask.NameToLayer("Player"));

            if (playerPos != null)
            {
                var directionToPlayer = (playerPos.gameObject.transform.position - transform.position).normalized;
                var motionToPlayer = directionToPlayer * Speed;
                rigidBody.velocity = motionToPlayer;
            }
            else
            {
                var hit = Physics2D.OverlapCircleAll(transform.position, TriggerRadius, Layer).FirstOrDefault();
                var direction = (hit.gameObject.transform.position - transform.position).normalized;
                var motion = direction * Speed;
                rigidBody.velocity = motion;
            }
        }  
    }
}
