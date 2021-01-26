using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private Transform groundcheck;
    private float groundDistance = 0.5f;
    [SerializeField] private LayerMask groundMask;

    //estados de los agentes
    [HideInInspector] public MonoBehaviour MovingState; //movimiento
    [HideInInspector] public MonoBehaviour AttackingState; //ataque
    [HideInInspector] public MonoBehaviour CalmState; //calma
    [HideInInspector] public MonoBehaviour InitState; //estado inicial
    [HideInInspector] public MonoBehaviour CurrentState; //estado actual

    // Start is called before the first frame update
    void Start()
    {
        MovingState = GetComponent<Moving>();
        AttackingState = GetComponent<Attacking>();
        CalmState = GetComponent<Calm>();

        InitState = CalmState;

        CurrentState = InitState;
        CurrentState.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
        if (!isGrounded)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }

    }

    public void ActivateState(MonoBehaviour nuevoEstado)
    {
        CurrentState.enabled = false;
        CurrentState = nuevoEstado;
        CurrentState.enabled = true;
    }

}
