using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapScript : MonoBehaviour
{
    public int maxScale = 15;
    public int minScale = 5;

    private Camera selfCam;

    private void Start()
    {
        selfCam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.M))
        {
            if (selfCam.orthographicSize + 1 <= maxScale)
            {
                selfCam.orthographicSize++;
                Debug.Log("Add Scale");
            }
        }

        else if (Input.GetKey(KeyCode.N))
        {
            if (selfCam.orthographicSize - 1 >= minScale)
            {
                selfCam.orthographicSize--;
                Debug.Log("Substruct scale");
            }
        }
    }
}
