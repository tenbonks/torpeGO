using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) 
        {
            Debug.Log("You Hit Escape");
            Application.Quit();
        }
    }
}
