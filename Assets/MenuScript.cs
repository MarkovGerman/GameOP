using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject.Find("Menu").SetActive(true);
        }
    }

    public void ResumeClicked()
    {
        GameObject.Find("Menu").SetActive(false);
    }

    public void ExitClicked()
    {
        SceneManager.LoadScene("StartScene");
    }
}
