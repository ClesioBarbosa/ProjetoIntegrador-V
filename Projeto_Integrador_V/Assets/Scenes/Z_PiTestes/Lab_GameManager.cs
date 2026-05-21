using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Lab_GameManager : MonoBehaviour
{
    [Header("Configuração timer e extras")]
    [SerializeField] private TMP_Text timeTxt;
    [SerializeField] private float timeL = 60f;
    [SerializeField] private Image anunciosSprite;

    [Header("Configuração anúncios certos")]
    [SerializeField] private Sprite [] anunciosSpritesCertos;
    
    [Header("Configuração anúncios errados")]
    [SerializeField] private Sprite [] anunciosSpritesErro;

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

    public void LadosAnunciosEsquerda()
    {
        if(EsquerdaDireita==0) //Esquerda lado certo
        {
            anunciosSprite.sprite=anunciosSpritesCertos[Random.Range(0,4)];
            anunciosSprite.enabled=true;
        }
        if(EsquerdaDireita==1) //Esquerda lado errado
        {
            
        }
    }

    public void FecharAnuncio()
    {
        anunciosSprite.enabled=false;
    }
}
