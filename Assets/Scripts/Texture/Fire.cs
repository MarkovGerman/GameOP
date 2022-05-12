using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float Damage = 1;
    public int AttackRange = 100;
    public LayerMask[] Mask;

    public bool fired;

    public void Start()
    {
        Mask = new LayerMask[] { LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Solid") };
    }

    void Update()
    {
        if (fired)
        {
            Collider2D[] mobs = Physics2D.OverlapCircleAll(transform.position, AttackRange, Mask[0]);

            for (int i = 0; i < mobs.Length; i++)
            {
                mobs[i].GetComponent<Health>().SelfHealth -= Damage;
            }

            mobs = Physics2D.OverlapCircleAll(transform.position, AttackRange, Mask[1]);

            for (int i = 0; i < mobs.Length; i++)
            {
                mobs[i].GetComponent<Health>().SelfHealth -= Damage;
            }

            Destroy(gameObject);
        }
    }
}
