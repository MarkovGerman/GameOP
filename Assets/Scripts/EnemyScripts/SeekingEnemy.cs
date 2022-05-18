using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingEnemy : MonoBehaviour
{
    public GameObject Hero;
    public int Speed;

    private Rigidbody2D rigidBodyComponent;

    private void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        Speed *= 10;
    }

    void Update()
    {
        var direction = (Hero.transform.position - transform.position).normalized;
        var motion = direction * Speed;

        rigidBodyComponent.velocity = motion;
    }
}
