using System.Collections;
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
    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            Debug.Log("J'attaque");
            //On peut attaquer
            
                //Crée un cercle à une position et rayon définie et tous les ennemis
                //se trouvant dans cette 
                Collider2D enemiesToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);               
            if(enemiesToDamage != null)
            {
                enemiesToDamage.GetComponent<PlayerScript>().TakeDamage(damage);

            }


            timeBtwAttack = startTimeBtwAttack;
        }
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
