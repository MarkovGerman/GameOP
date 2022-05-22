using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JumpingBullet : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    public float Distance;
    public int Damage = 1;
    public LayerMask whatIsSolid;
    public string EnemyTag;

    private Tilemap walls;

    private void Start()
    {
        walls = GameObject.Find("walls").GetComponent<Tilemap>();
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, Distance, whatIsSolid);

        LifeTime -= Time.deltaTime;

        var tile = walls.GetTile(walls.WorldToCell(gameObject.transform.position));

        if (tile != null && tile.name.Substring(0, 4) == "wall4")
            Destroy(gameObject);

            if (LifeTime <= 0 || Distance <= 0)
        {
            Destroy(gameObject);
        }

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag(EnemyTag))
            {
                //hitInfo.collider.GetComponent<EnemyInteraction>().TakeDamage(Damage);
            }
            Destroy(gameObject);
        }

        //if (tile != null && tile.name.Substring(0, 4) == "wall4")
        //    transform.Translate(-Vector2.right * Speed * Time.deltaTime);
        //
        //else 
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        Distance -= Speed * Time.deltaTime;
    }
}
