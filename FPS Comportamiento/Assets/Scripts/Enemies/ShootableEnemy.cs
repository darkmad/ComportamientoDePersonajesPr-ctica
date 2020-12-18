using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShootableEnemy : MonoBehaviour
{

    [SerializeField] VisualEffect vfx; 
    private int currentHealth = 3;

    
    public void Damage(Vector3 hit, int damageAmount)
    {
        vfx.transform.position = hit;
        vfx.Play();

        currentHealth -= damageAmount;
        Debug.Log("Vida: " + currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
