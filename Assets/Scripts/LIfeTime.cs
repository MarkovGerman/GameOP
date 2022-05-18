using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIfeTime : MonoBehaviour
{
    public float LifeTime;
    public LayerMask Layer;
    private float curTime;

    void Start()
    {
        curTime = 0f;
        LifeTime *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (curTime >= LifeTime)
            Destroy(gameObject);
        curTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Destroy(gameObject);
    }
}
