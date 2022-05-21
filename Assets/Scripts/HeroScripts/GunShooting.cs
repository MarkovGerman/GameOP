using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public float offset = -90;
    public GameObject bullet;
    public Transform shootPoint;
    public string Enemy = "Enemy";

    private float timeBetweenShoots;
    public float startTimeBetweenShoots = 5f;

    void FixedUpdate()
    {
        var diff = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(diff.x, -diff.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        if (timeBetweenShoots <= 0)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            {
                var bul= Instantiate(bullet, shootPoint.position, transform.rotation);

                bul.GetComponent<BulletBehaviour>().EnemyTag = Enemy;

                timeBetweenShoots = startTimeBetweenShoots;
            }
        }
        else
        {
            timeBetweenShoots -= Time.deltaTime;
        }
    }
}
