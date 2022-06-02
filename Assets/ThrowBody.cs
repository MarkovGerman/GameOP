using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBody : MonoBehaviour
{
    private Player controller;
    private Rigidbody2D rb;
    
    private void Start()
    {
        controller = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Throw(float power, Vector2 destination)
    {
        controller.IsOffed = true;
    }
    
}
