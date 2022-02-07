using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        PlayerData data = SaveSystem.loadPlayer();
        player.level = data.level;
    }
}
