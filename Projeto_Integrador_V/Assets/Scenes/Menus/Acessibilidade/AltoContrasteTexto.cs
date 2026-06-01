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
    [SerializeField] private Color [] color;

    void Start()
    {
        if(alto_contrasteBool==false)
        {
            for(int i=0; i < caixafundo.Length; i++)
            {
                caixafundo[i].color=color[0];
            }

            for(int i=0; i < texto.Length; i++)
            {
                texto[i].color=color[2];
            }
        }
        else
        {
            for(int i=0; i < caixafundo.Length; i++)
            {
                caixafundo[i].color=color[1];
            }

            for(int i=0; i < texto.Length; i++)
            {
                texto[i].color=color[3];
            }
        }
    }
}
