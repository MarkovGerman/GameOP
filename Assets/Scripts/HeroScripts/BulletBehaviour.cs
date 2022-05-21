using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float Speed;
    public string Enemy;
    public float LifeTime;
    public float Distance;
    public int Damage = 1;
    public LayerMask whatIsSolid;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, Distance, whatIsSolid );

        LifeTime -= Time.deltaTime;

        if (LifeTime <= 0 || Distance <= 0)
        {
            Destroy(gameObject);
        }

        if (hitInfo.collider != null)
        {
            Debug.Log(hitInfo.collider);
            
            if (hitInfo.collider.CompareTag("Enemy") && Enemy == "Enemy")
            {
                hitInfo.collider.GetComponent<EnemyInteraction>().TakeDamage(Damage);
                //Debug.Log("Hit");
            }
            if (hitInfo.collider.CompareTag("Player") && Enemy == "Player")
            {
                hitInfo.collider.GetComponent<Health>().SelfHealth -= Damage;
            }            

            // Debug.Log("Hit smth");
            // if (hitInfo.collider.CompareTag("Tank"))
            // {
            //     hitInfo.collider.GetComponent<Fire>().fired = true;
            // }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        Distance -= Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider){}
}
