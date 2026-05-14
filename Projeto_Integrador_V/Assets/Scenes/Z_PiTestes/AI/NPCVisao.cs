using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVisao : MonoBehaviour
{
    [Header("Configurações de Visão")]
    [SerializeField] float raioDeVisao = 10f;
    [Range(0, 180)]
    [SerializeField] float anguloDeVisao = 60f;
    [SerializeField] int quantidadeRaios = 20;

    [Header("Referência ao Player")]
    [SerializeField] Transform player;

    [Header("Camadas que bloqueiam visão")]
    [SerializeField] LayerMask mascaraObstaculos;

    [HideInInspector] public bool playerDetectado = false;

    void Update()
    {
        if (player == null) return;
        bool achou=false;

        Vector3 origem = transform.position;

        for (int i = 0; i < quantidadeRaios; i++)
        {
            float anguloAtual = -anguloDeVisao / 2f + (anguloDeVisao / quantidadeRaios) * i;

            Vector3 direcao = Quaternion.Euler(0, anguloAtual, 0) * transform.forward;

            RaycastHit hit;

            if (Physics.Raycast(origem, direcao, out hit, raioDeVisao, -mascaraObstaculos))
            {
                if (hit.transform == player)
                {
                    achou=true;
                    Debug.Log("ACHOU");
                    break;
                }
            }
        }

        NPCController npc = GetComponent<NPCController>();

        if(achou)
        {
            playerDetectado=true;
            npc.tempoParaPerderPlayer=0f;
        }
        else
        {
            npc.tempoSemVerPlayer += Time.deltaTime;

            if(npc.tempoSemVerPlayer >= npc.tempoParaPerderPlayer)
            {
                playerDetectado=false;
            }
        }
    }

    IEnumerator EsperarDetectao(float time)
    {
        yield return new WaitForSeconds(time);
        
    }

    void OnDrawGizmosSelected()
    {
        Vector3 origem = transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origem, raioDeVisao);

        for (int i = 0; i < quantidadeRaios; i++)
        {
            float anguloAtual = -anguloDeVisao / 2f + (anguloDeVisao / quantidadeRaios) * i;
            Vector3 direcao = Quaternion.Euler(0, anguloAtual, 0) * transform.forward;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(origem, direcao * raioDeVisao);
        }
    }
}
