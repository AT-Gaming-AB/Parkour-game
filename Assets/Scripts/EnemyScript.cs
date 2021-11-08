using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    public LayerMask layerMask;
    RaycastHit hit;
    private bool Raycast;
    public PlayerHealth playerHealthScript;
    [SerializeField] float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Raycast = Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 50f);
        // Debug.Log(Raycast);
        if(Raycast)
        {
            if(hit.transform.tag == "Player")
            {
                if (timer <= 0)
                {
                    Debug.Log("Player spotted");
                    playerHealthScript.playerHealth -= 30;
                }
                timer = 1;
                
            }
            else
            {
                Debug.Log("Hit other");
            }
        }
    }
}
