using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour
{

    public Text timerText;

    public Text scoreText;

    public int Score;

    private float secondsCount;
    private float minutesCount;

    private int totalSeconds;

    void Start()
    {
        timerText.text = "00:00";
        scoreText.text = "00";
    }
    
    void Update()
    {
        UpdateTheUI();
        if (Score >= 8)
        {
            if(PlayerPrefs.HasKey("bestTime"))
                if(totalSeconds < PlayerPrefs.GetInt("bestTime"))
                    PlayerPrefs.SetInt("bestTime",totalSeconds);
            else PlayerPrefs.SetInt("bestTime",totalSeconds);
            timerText.text = "GG";
            scoreText.text = "GG";
            StartCoroutine(ExampleCoroutine());
        }
    }
    
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("StartScene");
    }

    public void UpdateTheUI()
    {
        secondsCount += Time.deltaTime;
        totalSeconds += (int)secondsCount;
        timerText.text = minutesCount + ":" + (int)secondsCount;
        if (secondsCount >= 60)
        {
            minutesCount++;
            secondsCount = 0;
        }
        scoreText.text = ""+Score;
    }
}
