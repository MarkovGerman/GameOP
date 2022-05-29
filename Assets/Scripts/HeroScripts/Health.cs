using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public bool Cheat;
    public float SelfHealth;

    private void Update()
    {
        if (Cheat)
            SelfHealth = 100;
    }
}
