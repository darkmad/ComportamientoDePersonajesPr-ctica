using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehavoiur : MonoBehaviour
{
    private Transform playerHead;
    [SerializeField] private bool playerDetected = false;
    public NavMeshAgent agent;
    private bool covered = false;
    private GameObject cover;
    public LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHead = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
        //agent.SetDestination(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            if (!covered)
            {

            }else if (covered && transform.position != cover.transform.position)
            {
                agent.SetDestination(cover.transform.position);
            }
        }
    }

    public void startFight()
    {
        Debug.Log("La pelea empieza");
        playerDetected = true;
    }



    private void OnTriggerStay(Collider collision)
    {
        if (!covered)
        {
            if (collision.gameObject.tag.Equals("Cover"))
            {
                Debug.Log("Covertura detectada ");
                for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                {
                    RaycastHit h;

                    if (Physics.Raycast(collision.gameObject.transform.GetChild(i).position, (playerHead.transform.position - collision.gameObject.transform.GetChild(i).position).normalized, out h, (playerHead.position - collision.gameObject.transform.GetChild(i).position).magnitude, whatIsEnemy))
                    {
                        

                        if (!h.transform.gameObject.Equals(playerHead.gameObject) && !covered)
                        {
                            if (!collision.gameObject.GetComponent<CoverData>().positions[i])
                            {
                                Debug.Log("Tomo covertura");
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
