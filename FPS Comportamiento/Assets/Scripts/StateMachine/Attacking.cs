using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : MonoBehaviour
{
    //daño base de los enemigos
    public int damage = 5;

    private StateMachine stateMachine;
    private GameObject player;
    private HPBehaviour playerHP;
    public LayerMask whatIsEnemy;

    //contadores del ataque de melé
    private float meleeAttackCD = 0.6f;
    private float meleeAttackCount = 0;

    //tiempos de intervalo de disparos
    public float minShootTime = 0.5f;
    public float maxShootTime = 1.5f;

    //objeto agente
    public NavMeshAgent agent;
    //etiqueta del agente
    private string agentType;

    //variables chasePlayer
    public float leastDistance = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<HPBehaviour>();

        agentType = NavMesh.GetSettingsNameFromID(agent.agentTypeID);


        if(!agentType.Equals("Melee"))
            Invoke("shoot", Random.Range(minShootTime, maxShootTime));
    }

    //funcion disparo de los enemigos
    private void shoot()
    {
        
        RaycastHit h;
        Physics.Raycast(transform.GetChild(1).position, (player.transform.GetChild(0).position - transform.GetChild(1).position), out h, whatIsEnemy);
        if (player.transform.GetChild(0).gameObject != null && h.transform != null)
        {
            if (h.transform.gameObject.Equals(player.transform.GetChild(0).gameObject))
            {
                playerHP.getDamage(damage);
            }

            if (!agentType.Equals("Melee"))
                Invoke("shoot", Random.Range(minShootTime, maxShootTime));
        }
    }

    // Update is called once per frame
    void Update()
    {      
        transform.LookAt(player.transform);
        switch (agentType)
        {
            case "Agresive":
                chasePlayer();
                break;
            case "Melee":
                meleeAttackCount += Time.deltaTime;
                if ((player.transform.position - transform.position).magnitude >= leastDistance)
                {
                    stateMachine.ActivateState(stateMachine.MovingState);
                }
                else
                {                    
                    if (meleeAttackCount >= meleeAttackCD)
                    {
                        meleeAttackCount = 0;
                        shoot();
                    }
                }
                break;
        }
    }

    //funcion de persecucion enemigo arma de fuego
    void chasePlayer()
    {
        if((player.transform.position - transform.position).magnitude >= leastDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    ////funcion de persecucion enemigo a melé
    //void meleeChasePlayer()
    //{
    //    if ((player.transform.position - transform.position).magnitude >= leastDistance)
    //    {
    //        agent.isStopped = false;
    //        agent.SetDestination(player.transform.position);
    //    }
    //    else
    //    {
    //        agent.isStopped = true;

    //        if (meleeAttackCount >= meleeAttackCD)
    //        {
    //            meleeAttackCount = 0;
    //            shoot();
    //        }
    //    }
    //}
}
