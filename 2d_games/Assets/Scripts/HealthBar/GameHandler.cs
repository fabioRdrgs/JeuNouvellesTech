// https://www.youtube.com/watch?v=Gtw7VyuMdDc
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private HealthBar healthBar;

   // [HideInInspector]
    public int healthValue;
    // Start is called before the first frame update
    private void Start()
    {
        healthValue = 100;
    }

    private void FixedUpdate()
    {
     
            healthBar.SetSize( (float)healthValue/(float)100);    
    }
    
    public void Damage(int damage)
    {
        Debug.Log(healthValue);
        if (healthValue > 0)
            healthValue -= damage;

        if (healthValue < 0)
            healthValue = 0;
    }

    public void Die()
    {
        Damage(healthValue);
    }
}
