 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : FSMState
{
    private float tempoSemVer=0f;
    private float tempoMaxSemVer= 1.5f;
    public ChaseState()
    {
        stateID = FSMStateID.Chase;
    }

    public override void Reason(GameObject player, GameObject npc, bool detectao)
    {
        
        if(detectao)
        {
            tempoSemVer=0f;
            if(npc.GetComponent<NPCController>().coli==true) npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.CloseCombat);
        }
        else
        {
            tempoSemVer += Time.deltaTime;

            if(tempoSemVer >= tempoMaxSemVer)
            {
                //Debug.Log("PERDEU JOGADOR");
                tempoSemVer=0f;
                npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.LostPlayer);
            }
        }
    }

    public override void Act(GameObject player, GameObject npc, bool detectao)
    {
        npc.transform.LookAt(player.transform);
        npc.GetComponent<NavMeshAgent>().destination = player.transform.position;
        //Debug.Log("PERSEGUE");
    }
}
