using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<MushroomAI>().ResetPath();
    }
}
