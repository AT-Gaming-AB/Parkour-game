using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_1 : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "port1")
        {
            Debug.Log("Port1");   
        }
        else if (col.tag == "port2")
        {
            Debug.Log("Port2");   
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
