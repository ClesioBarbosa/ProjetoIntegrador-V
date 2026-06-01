using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Lab_GameManager : MonoBehaviour
{
    [Header("Configuração timer e extras")]
    [SerializeField] private TMP_Text timeTxt;
    private float timeInicial=90;
    [SerializeField] private float timeL = 90f;
    [SerializeField] private Image anunciosSprite, X;

    [Header("Configuração anúncios certos")]
    [SerializeField] private Sprite [] anunciosSpritesCertos;
    
    [Header("Configuração anúncios errados")]
    [SerializeField] private Sprite [] anunciosSpritesErro;

    [Header("Configuração dificuldade")]
    public static int dificuldadeLab;
    [HideInInspector] public int EsquerdaDireita=-1;

    void Start()
    {
        anunciosSprite.enabled=false;
        X.enabled=false;

        //Esquerda 0, Direita 1
        EsquerdaDireita = Random.Range(0,2);
        Debug.Log(EsquerdaDireita);

        if(dificuldadeLab*3 < 30)
        {
            timeL -= dificuldadeLab*3;
        }
        else timeL = 60;
        
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
            timeL=90;
            SceneManager.LoadScene("TelaResultadosSave");
            dificuldadeLab=0;
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
            anunciosSprite.enabled=true;
            X.enabled=true;
            anunciosSprite.sprite=anunciosSpritesCertos[Random.Range(0,anunciosSpritesCertos.Length)];
            
        }
        if(EsquerdaDireita==1) //Esquerda lado errado
        {
            anunciosSprite.enabled=true;
            X.enabled=true;
            anunciosSprite.sprite=anunciosSpritesErro[Random.Range(0,anunciosSpritesErro.Length)];
            
        }
    }

    public void LadosAnunciosDireita()
    {
        if(EsquerdaDireita==0) //Direita lado errado
        {
            anunciosSprite.sprite=anunciosSpritesErro[Random.Range(0,anunciosSpritesErro.Length)];
            anunciosSprite.enabled=true;
        }
        if(EsquerdaDireita==1) //Direita lado certo
        {
            anunciosSprite.sprite=anunciosSpritesCertos[Random.Range(0,anunciosSpritesCertos.Length)];
            anunciosSprite.enabled=true;
        }
    }

    public void FecharAnuncio()
    {
        anunciosSprite.enabled=false;
        X.enabled=false;
    }
}
