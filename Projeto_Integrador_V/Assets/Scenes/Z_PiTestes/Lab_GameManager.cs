using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Lab_GameManager : MonoBehaviour
{
    [Header("Configuração timer")]
    [SerializeField] private TMP_Text timeTxt;
    [SerializeField] private float timeL = 60f;
    [SerializeField] private Image anunciosSprite;

    [Header("Configuração anúncios certos")]
    [SerializeField] private GameObject [] anuncionsEsquerdaObj;
    [SerializeField] private GameObject [] anuncionsDireitaObj;
    [SerializeField] private Image [] anunciosSpritesCertos;
    [Header("Configuração anúncios errados")]
    [SerializeField] private GameObject anuncionsEsquerdaErroObj;
    [SerializeField] private GameObject anuncionsDireitaErroObj;
    [SerializeField] private Image [] anunciosSpritesErro;

    private int EsquerdaDireita=-1;
    void Start()
    {
        //Esquerda 0, Direita 1
        EsquerdaDireita = Random.Range(0,2);

        
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
