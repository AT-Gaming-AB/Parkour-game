using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    timer Timer;
    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        Timer = FindObjectOfType<timer>();
        player = FindObjectOfType<PlayerMovement>();
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            Timer.enabled = false;

            Scene scene = SceneManager.GetActiveScene();
            PlayerData data = SaveSystem.loadPlayer();
            if (scene.buildIndex+1 > data.level)
            {
                player.level = scene.buildIndex + 1;
                SaveSystem.SavePlayer(player);
            }
        }
    }
}
