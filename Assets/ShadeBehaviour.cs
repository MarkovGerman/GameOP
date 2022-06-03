using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadeBehaviour : MonoBehaviour
{
    public void ResumeButtonClicked()
    {
        GameObject.Find("Menu").SetActive(false);
    }

    public void ExitButtonClicked()
    {
        SceneManager.LoadScene("StartScene");
    }
}
