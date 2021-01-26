using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupBehaviour : MonoBehaviour
{
    private GameObject[] enemies;
    bool fightStarted = false;

    private void Start()
    {
        enemies = new GameObject[transform.childCount];
        for (int i=0; i<enemies.Length; i++)
        {
            enemies[i] = transform.GetChild(i).gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && !fightStarted)
        {
            foreach (GameObject go in enemies)
            {
                StateMachine st = go.GetComponent<StateMachine>();
                st.ActivateState(st.MovingState);
                
            }
            fightStarted = true;
        }
    }


}
