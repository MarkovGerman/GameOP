using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public PlayerInteraction parametrPlayer;
    

    void Start(){
        slider.minValue = 0;
        slider.maxValue = 100; 
    }
    
    void Update()
    {
        slider.value = parametrPlayer.Health;
    }
}
