using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting1 : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    public Vector3 playerPosition;
    public float offset = -90;
    float fireRate;
    float nextFire;
    public float waitTime = 20f;
    float timer;

    Animator anim;

    void Start()
    {
        timer = 0f;
        waitTime *= Time.deltaTime + Random.Range(1, 10);
        fireRate = 1f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0f)
        {
            playerPosition = GameObject.Find("Player").transform.position;
            Vector3 diff = playerPosition - transform.position;
            float angle = Mathf.Atan2(diff.x, -diff.y) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, angle + offset);
            if (Time.time > nextFire)
            {
                timer = waitTime;
                anim.SetBool("Shoot", true);
                Instantiate(bullet, transform.position, rotation);
                nextFire = Time.time + fireRate;
                GetComponent<SeekingEnemy>().StopTimer(60);
            }
        }
        else
        {
            timer -= Time.deltaTime;
            anim.SetBool("Shoot", false);
        }
    }


    void CheakifTimeToFire() {
        if(Time.time > nextFire) {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
