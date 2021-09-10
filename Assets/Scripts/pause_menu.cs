using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_menu : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    public GameObject thePlayer;
    BasicFPSController playerScript;
    public GameObject settingsmenu;
    public GameObject mainmenu;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = thePlayer.GetComponent<BasicFPSController>();
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame ()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        playerScript.canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void showsettings()
    {
        settingsmenu.SetActive(true);
        mainmenu.SetActive(false);
    }
    public void ChangeVolume(float value)
    {
        music.volume = value;
    }
}
