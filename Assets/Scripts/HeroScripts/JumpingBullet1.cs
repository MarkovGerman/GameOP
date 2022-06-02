using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JumpingBullet1 : MonoBehaviour
{
    public float LifeTime;
    public float Distance;
    public float Speed;
    public float Damage;
    public string EnemyTag;
    private Tilemap walls;

    [SerializeField] private AudioSource explosionSound;

    private Rigidbody2D rb;

    public void Start()
    {
        walls = GameObject.Find("walls").GetComponent<Tilemap>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Speed;
    }

    void FixedUpdate()
    {
        LifeTime -= Time.deltaTime;

        var tile = walls.GetTile(walls.WorldToCell(gameObject.transform.position));

        if (tile != null && tile.name.Substring(0, 4) == "wall") Reflect();

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

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Solid")) Reflect();
        explosionSound.Play();
    }

    private void Reflect()
    {
        var curAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(curAngles.x, curAngles.y, curAngles.z - 90f);
        rb.velocity = Vector3.zero;
        rb.velocity = transform.right * Speed;
    }
}
