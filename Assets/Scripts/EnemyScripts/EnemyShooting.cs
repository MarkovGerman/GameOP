using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    public Vector3 playerPosition;
    public float offset = -90;
    float fireRate;
    float nextFire;
    Animator anim;
    float waitTime = 120f;
    float timer;

    void Start()
    {
        waitTime *= Time.deltaTime;
        anim = GetComponent<Animator>();
        fireRate = 1f;
        nextFire = Time.time;
    }
    void Update()
    {
        if (timer >= 0f)
        {
            if (timer < 3 / 4 * waitTime)
                anim.SetBool("Shoot", true);
            else
                anim.SetBool("Shoot", false);
            timer -= Time.deltaTime;
        }
        else
        {
            playerPosition = GameObject.Find("Player").transform.position;
            Vector3 diff = playerPosition - transform.position;
            float angle = Mathf.Atan2(diff.x, -diff.y) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, angle + offset);
            if (Time.time > nextFire)
            {
                timer = waitTime;
                Instantiate(bullet, transform.position, rotation);
                nextFire = Time.time + fireRate;
                GetComponent<SeekingEnemy>().StopTimer(10);
            }
        }
    }
}
