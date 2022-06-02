using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTactic : MonoBehaviour
{
    public GameObject TriggerArea;
    public GameObject CentralPoint;

    public float MovingRadius;

    public float StepDelta = 0.1f;

    private bool playerInArea;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        playerInArea = TriggerArea.GetComponent<AreaDetector>().PlayerInArea;
        if (playerInArea)
            CheckRadius();
    }

    private void CheckRadius()
    {
        var curDistanceToPoint = Vector2.Distance(transform.position, CentralPoint.transform.position);

        var diff = curDistanceToPoint - MovingRadius;
        if (Mathf.Abs(diff) > 1f)
        {
            var targetVec = (CentralPoint.transform.position - transform.position).normalized * diff;
            Vector2.MoveTowards(transform.position, targetVec, StepDelta);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, CentralPoint.transform.position);
    }
}
