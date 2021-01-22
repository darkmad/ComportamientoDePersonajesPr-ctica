using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBehaviour : MonoBehaviour
{
    //vida actual
    public float currentHealth;
    //vida maxima
    public float maxHealth;
    //objeto barra de vida
    public GameObject healthBar;

    private Scrollbar bar;

    private void Start()
    {
        maxHealth = 100;
        currentHealth = 100;
        bar = healthBar.GetComponent<Scrollbar>();
    }

    public void getDamage(int amount)
    {
        if(maxHealth > 0)
        {
            currentHealth = currentHealth - amount;
            bar.size = currentHealth / maxHealth;
            //Debug.Log(currentHealth);
            if (maxHealth <= 0)
            {
                die();
            }
        }
        
    }

    public void getHealth(int amount)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth = currentHealth + amount;
            bar.size = currentHealth / maxHealth;
        }
    }

    public void die()
    {
        Debug.Log("Tas muerto");
    }
    
}
