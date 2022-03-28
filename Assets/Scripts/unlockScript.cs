using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockScript : MonoBehaviour
{
    PlayerMovement player;
    private string[] strArr;
    [SerializeField] GameObject portalEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        strArr = gameObject.tag.Split('t');
        Debug.Log(strArr[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.level < int.Parse(strArr[1]))
        {
            portalEffect.SetActive(false);
        }
    }
}
