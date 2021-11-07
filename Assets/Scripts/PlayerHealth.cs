using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public GameObject youdiedText;
    public GameObject youDiedCamera;
    public GameObject player;
    public int playerHealth = 100;
    private float decimalHealth = 100f;
    private int roundtoint;
    public TextMeshProUGUI tmpHealth;
    bool isLava;
    public bool lavaAudioPlayable = true;
    PlayerMovement movementScript;
    public LayerMask lavaMask;
    public float groundDistance = 0.5f;
    public Transform groundCheck;
    public AudioSource stepOnLava;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            Destroy(player);
            youDiedCamera.SetActive(true);
            youdiedText.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        isLava = Physics.CheckSphere(groundCheck.position, groundDistance, lavaMask);
        if (isLava)
        {
            decimalHealth -= (Time.deltaTime * 20);
            roundtoint = Mathf.RoundToInt(decimalHealth/10);
            playerHealth = roundtoint * 10;
            movementScript.AirJumpsReamaining = 0;
            playsound();
        }
        else
        {
            lavaAudioPlayable = true;
            stepOnLava.Stop();
        }
        tmpHealth.text = "Health: " + playerHealth;
        
    }
    void playsound()
    {
        if (lavaAudioPlayable)
        {
            stepOnLava.Play();
            lavaAudioPlayable = false;
        }
        
    }
}
