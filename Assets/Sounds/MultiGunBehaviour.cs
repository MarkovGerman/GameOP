using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiGunBehaviour : MonoBehaviour
{
    public float offset = -90;
    public GameObject bullet;
    public Transform shootPoint;

    private float timeBetweenShoots;
    public float startTimeBetweenShoots = 5f;
    [SerializeField] private AudioSource shootSound;

    private void Start()
    {
        startTimeBetweenShoots *= Time.deltaTime;
    }

    void FixedUpdate()
    {
        var diff = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(diff.x, -diff.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        if (timeBetweenShoots <= 0)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            {
                shootSound.Play();
                Instantiate(bullet, shootPoint.position, transform.rotation);
                timeBetweenShoots = startTimeBetweenShoots;
            }
        }
        else
        {
            timeBetweenShoots -= Time.deltaTime;
        }
    }
}
