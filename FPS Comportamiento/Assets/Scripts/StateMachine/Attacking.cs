﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : MonoBehaviour
{
    private StateMachine stateMachine;
    private GameObject player;
    private HPBehaviour playerHP;
    public LayerMask whatIsEnemy;

    private float minShootTime = 0.5f;
    private float maxShootTime = 1.5f;

    public NavMeshAgent agent;

    //variables chasePlayer
    private float leastDistance = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<HPBehaviour>();

        Invoke("shoot", Random.Range(minShootTime, maxShootTime));
    }


    private void shoot()
    {
        
        RaycastHit h;
        Physics.Raycast(transform.GetChild(1).position, (player.transform.GetChild(0).position - transform.GetChild(1).position), out h, whatIsEnemy);
        if (player.transform.GetChild(0).gameObject != null && h.transform != null)
        {
            if (h.transform.gameObject.Equals(player.transform.GetChild(0).gameObject))
            {
                playerHP.getDamage(2);
            }
            Invoke("shoot", Random.Range(minShootTime, maxShootTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (agent.agentTypeID)
        {
            case 1:
                chasePlayer();
                break;
        }
    }

    void chasePlayer()
    {
        if((player.transform.position - transform.position).magnitude >= leastDistance)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.Stop();
        }
    }
}
