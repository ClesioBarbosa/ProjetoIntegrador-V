using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButoesUINavegacao : MonoBehaviour
{
    [SerializeField] string cena;
    
    public void IrCena()
    {
        SceneManager.LoadScene(cena);
    }
}
