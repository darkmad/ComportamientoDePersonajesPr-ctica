using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    [HideInInspector] public MonoBehaviour CoveringState;
    [HideInInspector] public MonoBehaviour AttackingState;
    [HideInInspector] public MonoBehaviour CalmState;
    [HideInInspector] public MonoBehaviour InitState;
    [HideInInspector] public MonoBehaviour CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        CoveringState = GetComponent<Covering>();
        AttackingState = GetComponent<Attacking>();
        CalmState = GetComponent<Calm>();

        InitState = CalmState;

        CurrentState = InitState;
        CurrentState.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateState(MonoBehaviour nuevoEstado)
    {
        CurrentState.enabled = false;
        CurrentState = nuevoEstado;
        CurrentState.enabled = true;
    }

}
