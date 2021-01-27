using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moving : MonoBehaviour
{
    public Transform playerHead;
    public NavMeshAgent agent;
    private bool covered = false;
    private GameObject cover;
    private GameObject player;
    public LayerMask whatIsEnemy;

    private StateMachine stateMachine;

    private string agentType;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHead = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);

        player = GameObject.FindGameObjectWithTag("Player");

        stateMachine = GetComponent<StateMachine>();

        agentType = NavMesh.GetSettingsNameFromID(agent.agentTypeID);
        Debug.Log("ID" + agent.agentTypeID);
        //agent.SetDestination(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (agentType)
        {
            //enemigo que se cubre
            case "Humanoid":
                if (cover != null)
                {
                    agent.SetDestination(cover.transform.position);
                    //Debug.Log("going cover");
                }
                if (covered)
                {
                    covered = false;
                    stateMachine.ActivateState(stateMachine.AttackingState);
                }
                break;
            //enemigo que se lanza a atacar
            case "Agresive":
                stateMachine.ActivateState(stateMachine.AttackingState);
                break;
            case "Melee":
                meleeChasePlayer();
                break;
        }
    }

    void meleeChasePlayer()
    {
        if ((player.transform.position - transform.position).magnitude >= stateMachine.AttackingState.GetComponent<Attacking>().leastDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = true;
            stateMachine.ActivateState(stateMachine.AttackingState);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //Debug.Log("Dentro del trigger");
        if (!covered && playerHead != null)
        {
            if (collision.gameObject.tag.Equals("Cover"))
            {
                //Debug.Log("Covertura detectada ");
                for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                {
                    RaycastHit h;

                    if (Physics.Raycast(collision.gameObject.transform.GetChild(i).position, (playerHead.transform.position - collision.gameObject.transform.GetChild(i).position).normalized, out h, (playerHead.position - collision.gameObject.transform.GetChild(i).position).magnitude, whatIsEnemy))
                    {


                        if (!h.transform.gameObject.Equals(playerHead.gameObject) && !covered)
                        {
                            if (!collision.gameObject.GetComponent<CoverData>().positions[i])
                            {
                                //Debug.Log("Tomo covertura");
                                covered = true;
                                collision.gameObject.GetComponent<CoverData>().positions[i] = true;

                                cover = collision.gameObject.transform.GetChild(i).gameObject;
                            }
                        }
                    }
                }
                //cover = collision.gameObject;
            }
        }
    }
}
