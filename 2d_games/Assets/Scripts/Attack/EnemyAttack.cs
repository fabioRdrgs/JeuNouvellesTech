﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float timeBtwAttack;

    [SerializeField]
    public float startTimeBtwAttack;
    [SerializeField]
    public Transform attackPos;
    [SerializeField]
    public float attackRange;
    //Permet de déterminer ce qui est un ennemi ou non (Terrain, pnj amis, etc.)
    [SerializeField]
    public LayerMask whatIsEnemies;
    [SerializeField]
    public int damage;
    /// <summary>
    /// Appele la méthode à chaque images par secondes
    /// </summary>
    private void Update()
    {
        //Si le temps de recharge entre 2 attaques est = ou < à 0, permet d'attaquer
        if (timeBtwAttack <= 0)
        {

            //Crée un cercle à une position et rayon définie et tous les ennemis se trouvant dans cette zone prendront des dégats par rapport à ceux définis de base
            Collider2D enemiesToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);
            if (enemiesToDamage != null)
            {
                enemiesToDamage.GetComponent<PlayerScript>().TakeDamage(damage);
            }

            //Lance le temps de recharge entre 2 attaques
            timeBtwAttack = startTimeBtwAttack;
        }
        //Sinon, réduit le temps de recharge entre 2 attaques de manière graduelle jusqu'à 0
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }


    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
