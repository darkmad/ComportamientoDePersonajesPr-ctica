using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public int gunDamage=1;
    public float fireRate = 0.25f;
    public float weaponRange = 1000;
    public Transform gunEnd;
    public float counter=0;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    private AudioSource gunAudio;
    private float nextFire = 0.2f;

    public LayerMask whatToIgnore;

    //Variable para debug
    Vector3 destino;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(Input.GetButtonDown("Fire1") && counter > nextFire)
        {
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            
            if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, weaponRange, whatToIgnore))
            {
                destino = hit.point;
                Debug.Log("Entra" + hit.collider.gameObject.layer);
                ShootableEnemy health = hit.transform.GetComponent<ShootableEnemy>();
                if(health != null)
                {
                    health.Damage(hit.point, gunDamage);
                }
                else
                {
                    Debug.Log("No funciona");
                }
            }

            counter = 0;
        }
    }


    private  IEnumerator ShotEffect()
    {
        gunAudio.Play();

        yield return shotDuration;
    }
}
