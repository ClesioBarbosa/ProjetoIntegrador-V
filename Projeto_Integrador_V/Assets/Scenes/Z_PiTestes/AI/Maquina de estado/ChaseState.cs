 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : FSMState
{
    public ChaseState()
    {
        stateID = FSMStateID.Chase;
    }

    public override void Reason(GameObject player, GameObject npc, bool detectao)
    {
        if (!detectao){
            Debug.Log("PERDEU PERSEGUE");

            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.LostPlayer);
        }
        
        if(detectao && npc.GetComponent<NPCController>().coli==true)
        {
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.CloseCombat);
        }
    }

    public override void Act(GameObject player, GameObject npc, bool detectao)
    {
        npc.transform.LookAt(player.transform);
        npc.GetComponent<NavMeshAgent>().destination = player.transform.position;
        Debug.Log("PERSEGUE");
    }
}
