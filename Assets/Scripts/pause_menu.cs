using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pause_menu : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    public GameObject thePlayer;
    PlayerMovement playerScript;
    public GameObject gun;
    GunFire gunScript;
    public GameObject settingsmenu;
    public GameObject mainmenu;
    public Text title;
    public Text newGameText;
    public AudioSource music;
    private Scene scene;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = thePlayer.GetComponent<PlayerMovement>();
        pause.SetActive(false);
        gunScript = gun.GetComponent<GunFire>();
        gunScript.gamePaused = false;
        scene = SceneManager.GetActiveScene();
        title.text = scene.name;
        if (scene.name != "Main Island")
        {
            newGameText.text = "Restart";
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
        gunScript.gamePaused = true;
        Time.timeScale = 0;
        // playerScript.canMove = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    }

    public void ResumeGame ()
    {
        pause.SetActive(false);
        gunScript.gamePaused = false;
        Time.timeScale = 1;
        // playerScript.canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        settingsmenu.SetActive(false);
        mainmenu.SetActive(true);
    }
    public void quitGame(){
        Application.Quit();
    }
    public void showsettings(bool in_out)
    {
        settingsmenu.SetActive(in_out);
        mainmenu.SetActive(!in_out);
    }
    public void ChangeVolume(float value)
    {
        music.volume = value;
    }
    public void tryAgain()
    {
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }
}
