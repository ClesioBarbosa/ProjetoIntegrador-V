using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackState : FSMState
{
    bool atacou;
    public AttackState() { stateID = FSMStateID.Attack;}

    public override void Reason(GameObject player, GameObject npc, bool detectao)
    {
        if (Vector3.Distance(player.transform.position, npc.transform.position) > 1f)
        {
            atacou=false;
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.HitPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc, bool detectao)
    {
        if(!atacou)
        {
            npc.transform.LookAt(player.transform);
            Vector3 dirKnock = (npc.transform.position - player.transform.position).normalized;
            player.GetComponent<Rigidbody>().AddForce(dirKnock * 25 * -1,ForceMode.Impulse);
            player.GetComponent<LabMove>().vivo=false;
            atacou=true;
            Debug.Log("BATEU");
            SceneManager.LoadScene("TelaResultadosSave");
            Lab_GameManager.dificuldadeLab=0;
        }   
    }
}
