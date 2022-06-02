using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingEnemy : MonoBehaviour
{
    public GameObject TriggerArea;
    public float WaitTime;
    public float minDistance;
    public float RandomnessRange = 5f;

    private float waitTimer = 360;
    private Rigidbody2D rb;
    private Vector3 startPos;

    private float vecMagnitude = 1f;

    public float CircleRadius = 10f;

    private Vector2 target;
    private bool wait;

    public float SteeringTime;
    private float steeringTimer;

    private void Start()
    {
        steeringTimer = 0f;
        steeringTimer *= Time.deltaTime;

        wait = false;

        WaitTime *= Time.deltaTime;
        waitTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        GetComponent<EnemyShooting>().enabled = false;
    }

    void FixedUpdate()
    {
        if (FindPlayer()|| TriggerArea.GetComponent<AreaDetector>().PlayerInArea)
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

    private bool FindPlayer()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, CircleRadius);

        foreach(var hit in hits)
        {
            if (hit.gameObject.tag == "Player")
                return true;
        }

        return false;
    }

    private void TryToFindPath()
    {

        target = GameObject.Find("Player").transform.position;

        AddSteeringVec();
        AddFleeVecs();
    }

    private void AddSteeringVec()
    {
        var currDistance = (target - (Vector2)transform.position).magnitude;
        var desirableVelocity = (target - (Vector2)transform.position).normalized * vecMagnitude;

        if (currDistance < minDistance) desirableVelocity *= currDistance / minDistance;

        var steeringVec = desirableVelocity - rb.velocity;

        rb.velocity = rb.velocity + steeringVec;
        steeringTimer = 0f;
        
        steeringTimer += Time.deltaTime;
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
}
