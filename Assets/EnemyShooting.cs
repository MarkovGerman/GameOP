using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    public Vector3 playerPosition;
    public float offset = -90;
    float fireRate;
    float nextFire;
    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        Vector3 diff = playerPosition - transform.position;
        float angle = Mathf.Atan2(diff.x, -diff.y) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0f, 0f, angle + offset);
        if(Time.time > nextFire) {
            Instantiate(bullet, transform.position, rotation);
            nextFire = Time.time + fireRate;
        }
    }


    void CheakifTimeToFire() {
        if(Time.time > nextFire) {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
