using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "port1")
        {
            Debug.Log("Port1");
            SceneManager.LoadScene(1);
        }
        else if (col.tag == "port2")
        {
            Debug.Log("Port2");   
            SceneManager.LoadScene(2);
        }
        else if (col.tag == "port3")
        {
            Debug.Log("Port3");
        }
        else if (col.tag == "port4")
        {
            Debug.Log("Port4");   
        }
    }
}
