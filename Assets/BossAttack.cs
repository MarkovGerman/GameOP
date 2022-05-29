using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float Speed = 0.1f;
    public GameObject Bullet;
    public float Health;
    private float startHealth;
    Quaternion newRotation;
    public GameObject player;
    float smallAngle;
    public float TimeBtwShoots = 10f;
    private float offset = -90;
    private float curtime;

    private void Start()
    {
        curtime = 0f;
        TimeBtwShoots *= Time.deltaTime;
        player = GameObject.Find("Player");
        startHealth = gameObject.GetComponent<EnemyInteraction>().health;
    }

    [System.Obsolete]
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude <= 1)
        {
            Health = gameObject.GetComponent<EnemyInteraction>().health;
            if (Health < 0.2f * startHealth)
                HardShoot();
            AttackStandart();
        }
    }

    void HardShoot()
    {
        curtime += Time.deltaTime;
        TimeBtwShoots = 0.2f;
        if (curtime >= TimeBtwShoots)
        {
            var newRotation = new Quaternion();
            newRotation.SetAxisAngle(new Vector3(0, 0, 1),smallAngle);
            Instantiate(Bullet, transform.position, newRotation).GetComponent<BulletBehaviour>().Speed = 5f;

            newRotation.SetAxisAngle(new Vector3(0, 0, 1), 90f + smallAngle);

            Instantiate(Bullet, transform.position, newRotation).GetComponent<BulletBehaviour>().Speed = 5f;

            newRotation.SetAxisAngle(new Vector3(0, 0, 1), 180f + smallAngle);

            Instantiate(Bullet, transform.position, newRotation).GetComponent<BulletBehaviour>().Speed = 5f;
            smallAngle+=1f;

            curtime = 0f;
        }
    }

    void AttackStandart()
    {
        curtime += Time.deltaTime;
        TimeBtwShoots = 0.2f;
        if (curtime >= TimeBtwShoots)
        {
            var position = GameObject.Find("Player").transform.position;
            Vector3 diff = position - transform.position;
            float angle = Mathf.Atan2(diff.x, -diff.y) * Mathf.Rad2Deg;
            float smallAngle  = 30f;
            for (var i = 0; i < 6;i++)
            {
                var rotation = Quaternion.Euler(0f, 0f, angle + offset - smallAngle / 2 + i *smallAngle/6);
                Instantiate(Bullet, transform.position, rotation);
            }
            curtime = 0f; 
        }
    }
}
