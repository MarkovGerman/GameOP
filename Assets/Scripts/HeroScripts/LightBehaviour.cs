using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    public float offset = -90;
    public float range = 40;
    public GameObject lamp;

    // Update is called once per frame
    void Update()
    {
        Vector3 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // float angle = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
        // var vector = Quaternion.Euler(Vector3.Normalize(diff) * 40);
        float angle = Mathf.Atan2(dif.x, -dif.y) * Mathf.Rad2Deg;
        var diff = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * range;
        transform.rotation = Quaternion.Euler(-diff.y, diff.x, angle + offset);
    }
}
