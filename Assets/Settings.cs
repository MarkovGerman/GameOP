using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private bool isFull = true;
    public void FullScreentogglePressed()
    {
        isFull = !isFull;
        Screen.fullScreen = isFull;
    }
}
