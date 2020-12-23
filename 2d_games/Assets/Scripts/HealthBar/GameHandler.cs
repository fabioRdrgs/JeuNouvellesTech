// https://www.youtube.com/watch?v=Gtw7VyuMdDc
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private HealthBar healthBar;

    [HideInInspector]
    public int healthValue;
    /// <summary>
    /// Appelle la méthode avant la première image par seconde
    /// </summary>
    private void Start()
    {
        healthValue = 100;
    }
    /// <summary>
    /// Méthode appelée indépendament des images par secondes 
    /// </summary>
    private void FixedUpdate()
    {
        //Défini la taille du sprite de la barre de vie par rapport au nombre de points de vie restants
        healthBar.SetSize((float)healthValue / (float)100);
    }

    /// <summary>
    /// Méthode permettant d'effectuer des dommages à l'entité
    /// </summary>
    /// <param name="damage">Nombre de dégats voulus</param>
    public void Damage(int damage)
    {
        //Si les points de vie sont plus grand que 0, les réduits
        if (healthValue > 0)
            healthValue -= damage;
        //S'il sont plus bas que 0 les plaffone à 0
        if (healthValue < 0)
            healthValue = 0;
    }
    /// <summary>
    /// Méthod permettant de tuer instantanément l'entité
    /// </summary>
    public void Die()
    {
        Damage(healthValue);
    }

}
