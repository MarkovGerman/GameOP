using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int NumOfHearts;
    public Image[] Hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public float SelfHealth;
    public float Heal;

    void Update()
    {
        if (SelfHealth > NumOfHearts)
        {
            SelfHealth = NumOfHearts;
        }
        SelfHealth += Time.deltaTime * Heal;
        for (var i = 0; i < Hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(SelfHealth))
            {
                Hearts[i].sprite = FullHeart;
            }
            else
            {
                Hearts[i].sprite = EmptyHeart;
            }

            if (i < NumOfHearts)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
            if (SelfHealth < 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
          
    }
}
