using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startRoomScript : MonoBehaviour
{
    public Text timeText;
    void Start()
    {
        if (PlayerPrefs.HasKey("bestTime"))
        {
            var seconds = PlayerPrefs.GetInt("bestTime");
            var minutes = (int) (seconds / 60);
            seconds = seconds % 60;
            timeText.text = (int)minutes + ":" + (int)seconds;
        }
    }
}
