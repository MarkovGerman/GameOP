using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float attackDebuff;
    public float AttackStart;

    public Transform AttackPosition;
    public LayerMask Hero;
    public float AttackRange;
    public int Damage;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(AttackPosition.position, AttackRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackDebuff <= 0)
        {
            Collider2D[] hero = Physics2D.OverlapCircleAll(AttackPosition.position, AttackRange, Hero);

            for (int i = 0; i < hero.Length; i++)
            {
                hero[i].GetComponent<Player>().TakeDamage(Damage);
            }
            attackDebuff = AttackStart;
        }
        else
        {
            attackDebuff -= Time.deltaTime;
        }
    }
}
