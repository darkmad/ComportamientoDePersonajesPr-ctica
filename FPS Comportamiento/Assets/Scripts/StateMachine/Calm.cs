using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calm : MonoBehaviour
{
    private StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
