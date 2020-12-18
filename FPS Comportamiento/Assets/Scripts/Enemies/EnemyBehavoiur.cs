using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoiur : MonoBehaviour
{
    private bool playerDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {

        }
    }

    public void startFight()
    {
        Debug.Log("La pelea empieza");
        playerDetected = true;
    }
}
