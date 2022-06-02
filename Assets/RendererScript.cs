using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererScript : MonoBehaviour
{
    public GameObject Door;
    private SpriteRenderer sRenderer;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Door.GetComponent<DoorScript>().Opened)
        {
            sRenderer.color = new Color(0f, 100f, 0f);
        }
    }
}
