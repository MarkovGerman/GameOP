using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingEnemy : MonoBehaviour
{
    public GameObject TriggerArea;
    public float WaitTime;
    public float minDistance;
    public float RandomnessRange = 5f;

    private float StTimer;
    private float waitTimer = 360;
    private Rigidbody2D rb;
    private Vector3 startPos;

    private float vecMagnitude = 1f;

    public float CircleRadius = 10f;

    private Vector2 target;
    private bool wait;

    public float SteeringTime;
    private float oldX = 0f;
    private SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

        wait = false;

        WaitTime *= Time.deltaTime;
        waitTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        GetComponent<EnemyShooting>().enabled = false;
    }

    void FixedUpdate()
    {
        if (StTimer <= 0f)
        {
            UpdateFlip();
            if (TriggerArea.GetComponent<AreaDetector>().PlayerInArea)
            {
                TryToFindPath();
                GetComponent<EnemyShooting>().enabled = true;
            }

            else
            {
                GetComponent<EnemyShooting>().enabled = false;
                target = (Vector2)startPos + Random.insideUnitCircle * 10f;
                Wander();
            }
        }
        else
        {
            StTimer -= Time.deltaTime;
            rb.velocity = Vector2.zero;
        }
    }

    private void UpdateFlip()
    {
        if (transform.position.x - oldX > 0)
            render.flipX = true;
        else
            render.flipX = false;
        oldX = transform.position.x;

    }

    private void TryToFindPath()
    {

        target = GameObject.Find("Player").transform.position;
        if (Vector2.Distance(target, transform.position) > 2f)
        {
            AddSteeringVec();
            AddFleeVecs();
        }
        else rb.velocity = Vector2.zero;
    }

    private void AddSteeringVec()
    {
        var currDistance = (target - (Vector2)transform.position).magnitude;
        var desirableVelocity = (target - (Vector2)transform.position).normalized * vecMagnitude;

        if (currDistance < minDistance) desirableVelocity *= currDistance / minDistance;

        var steeringVec = desirableVelocity - rb.velocity;

        rb.velocity = rb.velocity + steeringVec;
    }

    private void Wander()
    {
        
        if (waitTimer > WaitTime || Vector2.Distance(transform.position, target) < RandomnessRange)
        {
            rb.velocity = Vector2.zero;

            if (!wait)
            {
                target = (Vector2)startPos + Random.insideUnitCircle * 20f;
                rb.velocity = (target - (Vector2)transform.position).normalized * vecMagnitude / 4f;
                waitTimer = 0f;
                Debug.Log(target);
            }
            wait = !wait;
        }

        waitTimer += Time.deltaTime;
    }

    private void AddFleeVecs()
    {
        var circleArea = Physics2D.OverlapCircleAll(transform.position, CircleRadius);

        foreach (var obj in circleArea)
        {
            var vec = (Vector2)(obj.transform.position - transform.position).normalized * vecMagnitude / 4;

            if (obj.gameObject.layer == LayerMask.NameToLayer("Solid") || obj.gameObject.layer == LayerMask.NameToLayer("Mob"))
                rb.velocity = (rb.velocity - vec).normalized * vecMagnitude;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CircleRadius);
    }

    public void StopTimer(int frames)
    {
        StTimer = Time.deltaTime * frames;
    }
}
