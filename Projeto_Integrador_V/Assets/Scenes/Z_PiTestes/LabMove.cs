using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LabMove : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float suavidade = 5f;
    //+publico -privado
    private Vector2 move;
    sbyte cimabaixo=0;
    [HideInInspector] public bool vivo=false;

    void Update()
    {
        if(vivo==true)
        {
            CimaBaixoToque();
            Vector2 tilt = new Vector2 (Input.acceleration.x, cimabaixo);

            // Suavização correta
            move.x = Mathf.Lerp(move.x, tilt.x, Time.deltaTime * suavidade);
            move.y = Mathf.Lerp(move.y, tilt.y, Time.deltaTime * suavidade);

            // Movimento no plano XZ
            Vector3 direcao = new Vector3(move.x, 0, move.y);
            transform.Translate(direcao * velocidade * Time.deltaTime, Space.World);
        }
    }

    void CimaBaixoToque()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            switch(touch.phase)
            {
                case TouchPhase.Began:
                if(touch.position.y > Screen.height / 2) {cimabaixo=1; Debug.Log("Cima");}
                else{cimabaixo=-1; Debug.Log("Baixo");} 
                break;

                case TouchPhase.Ended:
                cimabaixo=0;
                break;
            } 
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer==4) //Saida
        {
            //SceneManager.LoadScene("MenuMiniGames");
        }
        if(col.gameObject.layer==8) //Anuncios Esquerda
        {
            
        }
        if(col.gameObject.layer==9) //Anuncios Direita
        {
            
        }
    }
}
