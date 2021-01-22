using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBehaviour : MonoBehaviour
{
    //vida actual
    public int currentHealth;
    //vida maxima
    public int maxHealth;
    //objeto barra de vida
    public GameObject healthBar;

    private Scrollbar bar;

    private void Start()
    {
        maxHealth = 100;
        currentHealth = 100;
        bar = healthBar.GetComponent<Scrollbar>();
    }

    public void getDamage(int cantidad)
    {
        if(maxHealth > 0)
        {
            currentHealth = currentHealth -10;
            bar.size = currentHealth / maxHealth;
            if (maxHealth <= 0)
            {
                die();
            }
        }
        
    }

    public void getHealth(int cantidad)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth = currentHealth + 10;
            bar.size = currentHealth / maxHealth;
        }
    }

    public void die()
    {
        Debug.Log("Tas muerto");
    }
    
}
