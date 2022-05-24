using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAtackBehaviour : MonoBehaviour
{
    public float TimeBtwAttacks;
    public float AttackRadius;

    public float SwordDamage;

    public GameObject ShootPoint;
    public GameObject SwordPoint;
    public GameObject Bullet;

    private float attacksTimer;

    private void Start()
    {
        TimeBtwAttacks *= Time.deltaTime;
        attacksTimer = 0f;
    }

    void Update()
    {
        if (attacksTimer >= TimeBtwAttacks)
        {
            SwordAttack();
            attacksTimer = 0f;
        }
        
        if (attacksTimer >= TimeBtwAttacks / 4)
        {
            //Shoot();
        }

        attacksTimer += Time.deltaTime;
    }

    private void SwordAttack()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, AttackRadius);

        foreach(var hit in hits)
        {
            if (hit.gameObject.tag == "Player")
            {
                hit.gameObject.GetComponent<Health>().SelfHealth -= SwordDamage;
            }
        }
    }

    private void Shoot()
    {
        ChangeRotation();
        Instantiate(Bullet, ShootPoint.transform.position, transform.rotation);
    }

    private void ChangeRotation()
    {
        var sideBool = GetComponent<SpriteRenderer>().flipX;

        var curRotation = transform.rotation;

        if (sideBool)
        {
            curRotation.eulerAngles = new Vector3(0, 0, -100);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(SwordPoint.transform.position, AttackRadius);
    }
}
