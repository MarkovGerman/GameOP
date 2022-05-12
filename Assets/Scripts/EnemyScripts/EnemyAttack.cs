using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float attackDebuff;
    public float AttackStart;

    public Transform AttackPosition;
    public LayerMask[] Mob;
    public float AttackRange;
    public int Damage;

    private void Start()
    {
        Mob = new LayerMask[] { LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Solid") };
    }

    // Update is called once per frame
    void Update()
    {
        if (attackDebuff <= 0)
        {
            Collider2D[] hero = Physics2D.OverlapCircleAll(AttackPosition.position, AttackRange);

            for (int i = 0; i < hero.Length; i++)
            {
               //hero[i].GetComponent<Player>().TakeDamage(Damage);
            }
            attackDebuff = AttackStart;
        }
        else
        {
            attackDebuff -= Time.deltaTime;
        }
    }
}
