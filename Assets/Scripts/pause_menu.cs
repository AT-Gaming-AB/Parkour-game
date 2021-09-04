using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_menu : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    public GameObject thePlayer;
    BasicFPSController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = thePlayer.GetComponent<BasicFPSController>();
        // playerScript.canMove = false;
        pause.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                PauseGame();
            } else
            {
                ResumeGame();
            }
        }
    }
    void PauseGame ()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
        playerScript.canMove = false;
    }

    void ResumeGame ()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        playerScript.canMove = true;
    }
}
