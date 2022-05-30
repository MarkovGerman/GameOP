using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    private float health;
    private Slider slider;
    private float maxHealth;

    void Start()
    {
        slider = transform.Find("HealthBar/Panel/Slider").GetComponent<Slider>();
        health = GetComponent<Health>().SelfHealth;
        maxHealth = GetComponent<Health>().maxHealth;
    }

    void FixedUpdate()
    {
        health = GetComponent<Health>().SelfHealth;
        slider.value = health / maxHealth;
        Debug.Log(health + " " + slider.value);
    }
}
