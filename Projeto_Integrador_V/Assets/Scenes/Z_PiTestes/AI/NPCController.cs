using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public FSMSystem fsm;
    [SerializeField] Transform [] patrolPoints;
    [SerializeField] GameObject jogador;
    [HideInInspector] public bool coli;
    [HideInInspector] public float tempoSemVerPlayer = 0f;
    public float tempoParaPerderPlayer = 1.5f;
    
    private void Start()
    {
        InitFSM();
    }
    
    private void InitFSM()
    {
        // === Estado: Patrulha ===
        fsm = new FSMSystem();
        PatrolState patrol = new PatrolState(patrolPoints);
        fsm.AddState(patrol);
        patrol.AddTransition(FSMTransition.SawPlayer, FSMStateID.Chase);

        // === Estado: Perseguir ===
        ChaseState chase = new ChaseState();
        fsm.AddState(chase);
        chase.AddTransition(FSMTransition.LostPlayer, FSMStateID.Patrol);
        chase.AddTransition(FSMTransition.CloseCombat, FSMStateID.Attack);

        // === Estado: Atacar ===
        AttackState attack = new AttackState();
        fsm.AddState(attack);
        attack.AddTransition(FSMTransition.HitPlayer, FSMStateID.Happy);

        // === Estado: Feliz ===
        HappyState happy = new HappyState();
        fsm.AddState(happy);
    }
    
    private void Update()
    {
        if (fsm == null) return;            
        if (jogador != null)
        {
            fsm.Update(jogador, this.gameObject, this.gameObject.GetComponent<NPCVisao>().playerDetectado);
        }
    }

    void OnCollisionEnter(Collision col) { if(col.gameObject.CompareTag("Player")) coli=true;}
    void OnCollisionExit(Collision col){ if(col.gameObject.CompareTag("Player")) coli=false;}

}