using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class pickup : MonoBehaviour
{
    public GameObject gun;
    public GameObject text;
    public GameObject canvas; 
    public timerScript timer;
     void Start()
     {
        //  pickup.StartCoroutine(LateCall(sec));
     }
  
     IEnumerator LateCall(float seconds)
     {
        yield return new WaitForSeconds(seconds);

        text.SetActive(false);
     }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            canvas.SetActive(true);
            text.SetActive(true);
            timer.StartCoroutine(LateCall(5));
            gun.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
