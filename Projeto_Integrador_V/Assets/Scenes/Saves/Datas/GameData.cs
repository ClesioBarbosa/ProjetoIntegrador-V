using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
   public int iscaPontuacao,labirintoPontuacao,contatoPontuacao;
  
   public GameData()
    {
        this.iscaPontuacao=0; this.labirintoPontuacao=0;  this.contatoPontuacao=0;
    }
}
