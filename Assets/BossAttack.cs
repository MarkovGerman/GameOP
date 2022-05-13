using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float Speed = 0.1f;
    public GameObject Bullet;

    public float TimeBtwShoots = 10f;

    private float curtime;

    private void Start()
    {
        curtime = 0f;
        TimeBtwShoots *= Time.deltaTime;
    }

    void Update()
    {
        curtime += Time.deltaTime;
        if (curtime >= TimeBtwShoots)
        {
            Instantiate(Bullet, transform.position, transform.rotation);

            var newRotation = new Quaternion();
            newRotation.SetAxisAngle(new Vector3(0, 0, 1), 90f);

            Instantiate(Bullet, transform.position, newRotation);

            newRotation.SetAxisAngle(new Vector3(0, 0, 1), 180f);

            Instantiate(Bullet, transform.position, newRotation);


            curtime = 0f;
        }
    }
}
