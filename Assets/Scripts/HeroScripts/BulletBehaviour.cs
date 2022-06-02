using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletBehaviour : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    public float Distance;
    public float Damage = 1;
    public string EnemyTag;

    private Rigidbody2D rb;

    private Tilemap walls;

    private void Start()
    {
        walls = GameObject.Find("walls").GetComponent<Tilemap>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Speed;
    }

    void FixedUpdate()
    {
        LifeTime -= Time.deltaTime;

        var tile = walls.GetTile(walls.WorldToCell(gameObject.transform.position));

        if (tile != null && tile.name.Substring(0, 4) == "wall")
            Destroy(gameObject);

        if (LifeTime <= 0 || Distance <= 0)
        {
            Destroy(gameObject);
        }
        Distance -= Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit:" + collision.gameObject.name);

        if (collision.gameObject.CompareTag(EnemyTag))
        {
            collision.gameObject.GetComponent<Health>().SelfHealth -= Damage;
            Destroy(gameObject);
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Solid"))
        {
            Destroy(gameObject);
        }
    }
}
