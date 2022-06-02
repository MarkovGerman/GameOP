using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlierShooting : MonoBehaviour
{
    public GameObject Bullet;
    public float TimeShoots;
    public float Pause;
    private float time = 0f;
    public float Frequency = 5f;
    private float curtime = 0f;
    private float dir = 1f;
    private FlierWalking walking;
    public Vector2 movementVector;
    private GameObject player;
    void Start()
    {
        Frequency *= Time.deltaTime;
        walking = GetComponent<FlierWalking>();
    }

    void Update()
    {
        player = GameObject.Find("Player");
        if ((transform.position - player.transform.position).magnitude <= 5)
            Shoot();   
    }

    void Shoot()
    {
        movementVector = walking.MovementVector;
        var angle = Mathf.Atan2(movementVector.x, -movementVector.y) * Mathf.Rad2Deg;
        if (curtime >= Pause && curtime < TimeShoots + Pause)
        {
            if (time >= Frequency)
            {
                Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, angle - 90f));
                time = 0f;
            }
            else
                time += Time.deltaTime;
        }
        if (curtime >= TimeShoots + Pause)
        {
            curtime = 0f;
            movementVector *= -1;
        }
        curtime += Time.deltaTime; 
    }
}
