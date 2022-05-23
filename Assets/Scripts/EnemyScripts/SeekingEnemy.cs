using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingEnemy : MonoBehaviour
{
    public int Speed;
    public GameObject TriggerArea;
    public float WaitTime;

    private float waitTimer;
    private Rigidbody2D rb;
    private Vector3 startPos;

    private float vecMagnitude = 2f;

    public float circleRadius = 5f;
    public float DebugTimeBtw;

    private float DebugTimer;

    private void Start()
    {
        WaitTime *= Time.deltaTime;
        waitTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        Speed *= 2;
        startPos = transform.position;

        DebugTimeBtw *= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (TriggerArea == null || TriggerArea.GetComponent<AreaDetector>().PlayerInArea)
            TryToFindPath();

        else
        {
            if ((startPos - transform.position).magnitude >= 1.0f && waitTimer >= WaitTime)
            {
                var direction = (startPos - transform.position).normalized;
                rb.velocity = direction * Speed;
                waitTimer = 0f;
            }

            else
            {
                waitTimer += Time.deltaTime;
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void TryToFindPath()
    {
        DebugTimer += Time.deltaTime;
        
        AddSeekingVec();
        AddFleeVecs();

        if (DebugTimer >= DebugTimeBtw)
        {
            Debug.Log(rb.velocity);
            DebugTimer = 0f;
        }
    }

    private void AddSeekingVec()
    {
        var player = GameObject.Find("Player");

        var direction = (player.transform.position - transform.position).normalized;

        rb.velocity = ((Vector2)(direction) * vecMagnitude + rb.velocity).normalized;
    }

    private void AddFleeVecs()
    {
        var circleArea = Physics2D.OverlapCircleAll(transform.position, circleRadius);

        foreach (var obj in circleArea)
        {
            var vec = (obj.transform.position - transform.position).normalized;

            if (obj.gameObject.layer == LayerMask.NameToLayer("Solid"))
                rb.velocity = (rb.velocity - (Vector2)vec * vecMagnitude / 4).normalized;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
}
