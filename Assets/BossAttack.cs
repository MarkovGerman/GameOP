using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float Speed = 0.1f;
    public GameObject Bullet;

    private bool wentRight = false;
    private int ToGo = 1;

    private void Start()
    {
        
    }

    void Update()
    {
        var firstPos = transform.position;


        transform.position += new Vector3(10, 10, 0) * ToGo;
        ToGo *= -1;



        Instantiate(Bullet, transform.position, transform.rotation);
    }
}
