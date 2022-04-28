using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      /* void CheckHealth()
        {
            if (Health > NumOfHearts)
            {
                Health = NumOfHearts;
            }

            Health += Time.deltaTime * Heal;

            for (int i = 0; i < Hearts.Length; i++)
            {
                if (i < Mathf.RoundToInt(Health))
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

                if (Health < 1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }*/
    }
}
