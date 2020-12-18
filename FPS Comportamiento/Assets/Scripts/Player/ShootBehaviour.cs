using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public int gunDamage=1;
    public float fireRate = 0.25f;
    public float weaponRange = 500;
    public Transform gunEnd;
    public float counter=0;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    private AudioSource gunAudio;
    private float nextFire;


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
            if (Physics.Raycast(rayOrigin, transform.parent.forward, out hit, weaponRange))
            {
                Debug.Log("Entra");
                ShootableEnemy health = hit.collider.GetComponent<ShootableEnemy>();
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
