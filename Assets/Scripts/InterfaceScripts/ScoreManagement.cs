using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public int Score = 0;
    public Text DisplayedScore;

    void Update()
    {
        DisplayedScore.text = "Your score:" + Score.ToString();
    }

    public void Add()
    {
        Score++;
    }
}
