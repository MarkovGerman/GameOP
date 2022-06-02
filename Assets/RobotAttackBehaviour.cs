using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttackBehaviour : MonoBehaviour
{
    public float TimeBtwAttacks;
    public float AttackRadius;
    public float AttackPower;

    public float SwordDamage;

    public GameObject ShootPoint;
    public GameObject SwordPoint;
    public GameObject Bullet;

    private float attacksTimer;
    private float health;

    private void Start()
    {
        health = GetComponent<Health>().SelfHealth;
        TimeBtwAttacks *= Time.deltaTime;
        attacksTimer = 0f;
    }

    void Update()
    {
        health = GetComponent<Health>().SelfHealth;
        if (health <= 0f) Destroy(gameObject);
        SwordAttack();
        //Shoot();
    }

    private void SwordAttack()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, AttackRadius);

        foreach(var hit in hits)
        {
            Debug.Log(hit.gameObject.tag);
            if (hit.gameObject.tag == "Player")
            {
                var destination = (hit.gameObject.transform.position - transform.position).normalized;

                if (attacksTimer >= TimeBtwAttacks)
                {
                    hit.gameObject.GetComponent<Health>().SelfHealth -= SwordDamage;
                    hit.gameObject.GetComponent<Player>().OffController();
                    hit.gameObject.GetComponent<Rigidbody2D>().velocity += (Vector2)destination * AttackPower;
                    attacksTimer = 0f;
                }
                attacksTimer += Time.deltaTime;
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
