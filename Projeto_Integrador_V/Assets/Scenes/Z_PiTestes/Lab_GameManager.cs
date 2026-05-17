using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Lab_GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timeTxt;
    [SerializeField] float timeL = 60f;
    void Start()
    {
        
    }

    void Update()
    {
        if(timeL>0)
        {
            timeL -= Time.deltaTime;
            UpdateTimer(timeL);
        }
        else if (timeL <= 0)
        {
            timeL=60;
            SceneManager.LoadScene("MenuMiniGames");
        }
       
    }

    void UpdateTimer(float timeA)
    {
        timeA += 1;

        float min = Mathf.FloorToInt(timeA / 60);
        float sec = Mathf.FloorToInt(timeA % 60);
        timeTxt.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
