using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShootableEnemy : MonoBehaviour
{

    [SerializeField] VisualEffect vfx; 
    public int currentHealth = 3;

    
    public void Damage(Vector3 hit, int damageAmount)
    {
        vfx.transform.position = hit;
        vfx.Play();

        currentHealth -= damageAmount;
        Debug.Log("Vida: " + currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Invoke("destroyThis", 0.3f);
        }
    }
    public void destroyThis()
    {
        Destroy(transform.parent.gameObject);
    }
}
