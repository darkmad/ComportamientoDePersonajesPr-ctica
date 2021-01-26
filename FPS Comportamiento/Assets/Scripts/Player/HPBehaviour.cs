using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBehaviour : MonoBehaviour
{
    //vida actual
    public float currentHealth;
    //Referencia al menú de muerte
    [SerializeField] private GameObject deathMenu;
    //vida maxima
    public float maxHealth;
    //objeto barra de vida
    public GameObject healthBar;
    //ui barra de vida
    private Scrollbar bar;
    //para la recuperacion de vida
    public float cont = 0;
    public float timeToHeal = 1.5f;

    private void Start()
    {
        maxHealth = 100;
        currentHealth = 100;
        bar = healthBar.GetComponent<Scrollbar>();
    }
    
    //funcion donde restamos vida al jugador
    public void getDamage(int amount)
    {
        if(maxHealth > 0)
        {
            currentHealth = currentHealth - amount;
            bar.size = currentHealth / maxHealth;
            //Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                die();
            }
        }
        cont = 0;
        
    }

    //funcion de curacion del jugador
    public void getHealth(int amount)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth = currentHealth + amount;
            if (currentHealth > maxHealth) { currentHealth = maxHealth; }
            bar.size = currentHealth / maxHealth;

        }
    }

    private void Update()
    {
        cont += Time.deltaTime;
        if (cont >= timeToHeal)
        {
            getHealth(20);
            cont = 0;
        }
    }

    //funcion de muerte del jugador
    public void die()
    {
        //Debug.Log("Death");
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }
    
}
