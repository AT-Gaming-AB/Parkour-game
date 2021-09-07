using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    private float speed = 100f;
    public float lifeDuration = 1f;
    private float lifeTimer;
    GunFire GunScript;
    public LayerMask layerMask;
    public Transform TouchCheck;

    // Start is called before the first frame update
    void Start()
    {
        GunScript = FindObjectOfType<GunFire>();
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position += transform.forward * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f) {
            Destroy (gameObject);
        }
        if (Physics.CheckSphere(TouchCheck.position, 0.05f, layerMask))
        {
            Destroy (gameObject);
            Debug.Log("Bullet Destroyed");
        }
    }
}
