using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTactic : MonoBehaviour
{
    public GameObject TriggerArea;

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
    }

}
