using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public TextMeshProUGUI tmpTimer;
    private float Timer = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Timer += Time.deltaTime;
        tmpTimer.text = Timer.ToString("F1");
    }
}
