using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletBehaviour : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    public float Distance;
    public int Damage = 1;
    public LayerMask whatIsSolid;

    private Tilemap walls;

    private void Start()
    {
        walls = GameObject.Find("walls").GetComponent<Tilemap>();
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, Distance, whatIsSolid );

        LifeTime -= Time.deltaTime;

        var tile = walls.GetTile(walls.WorldToCell(gameObject.transform.position - new Vector3(0.5f, 0.5f)));

        if (tile != null && tile.name == "wall4")
            Destroy(gameObject);

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
}
