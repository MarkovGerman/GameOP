using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    public float Distance;
    public int Damage = 1;
    public LayerMask whatIsSolid;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, Distance, whatIsSolid );

        LifeTime -= Time.deltaTime;

        if (LifeTime <= 0 || Distance <= 0)
        {
            Destroy(gameObject);
        }

        if (hitInfo.collider != null)
        {
            Debug.Log(hitInfo.collider);
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<EnemyInteraction>().TakeDamage(Damage);
                //Debug.Log("Hit");
            }

            Debug.Log("Hit smth");
            if (hitInfo.collider.CompareTag("Tank"))
            {
                hitInfo.collider.GetComponent<Fire>().fired = true;
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        Distance -= Speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
