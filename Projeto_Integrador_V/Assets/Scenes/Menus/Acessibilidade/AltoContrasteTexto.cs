using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AltoContrasteTexto : MonoBehaviour
{
    public static bool alto_contrasteBool=false;
    [SerializeField] private Image [] caixafundo;
    [SerializeField] private  TMP_Text [] texto;
    [SerializeField] private Color [] color; // 0 cor texto branco, 1 cor fundo transparente, 2 cor texto amarelo, 3 cor fundo escuro

    void Start()
    {
        if(alto_contrasteBool==false)
        {
            for(int i=0; i < caixafundo.Length; i++)
            {
                caixafundo[i].color=color[1];
            }

            for(int i=0; i < texto.Length; i++)
            {
                texto[i].color=color[0];
            }
        }
        else
        {
            for(int i=0; i < caixafundo.Length; i++)
            {
                caixafundo[i].color=color[3];
            }

            for(int i=0; i < texto.Length; i++)
            {
                texto[i].color=color[2];
            }
        }
    }
}
