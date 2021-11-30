using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    timer Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = FindObjectOfType<timer>();
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            Timer.enabled = false;
        }
    }
}
