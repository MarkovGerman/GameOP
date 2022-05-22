using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int KeysNum = 0;
    public Text Text;


    void Update()
    {
        Text.text = KeysNum.ToString();
    }
}
