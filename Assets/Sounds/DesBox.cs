using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesBox : MonoBehaviour
{
    // Start is called before the first frame update
    public int countHit;

    // Update is called once per frame
    void Update()
    {
        if (countHit <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            countHit--;
    }
}
