using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack = 0;
    private float timeShieldUp;

    [SerializeField]
    private PlayerScript Player;
    [SerializeField]
    private PlayerMovement movement;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private LayerMask whatIsEnemies;

    [HideInInspector]
    public bool shieldUp;
    [HideInInspector]
    public bool canUseShield = true;
    [HideInInspector]
    public float shieldCooldown;
    [HideInInspector]
    public bool canTakeDamage = true;

    [SerializeField]
    public float timeShieldCooldown;
    [SerializeField]
    public float AttackCooldown;
    [SerializeField]
    public Transform attackPos;
    [SerializeField]
    public float attackRange;
    //Permet de déterminer ce qui est un ennemi ou non (Terrain, pnj amis, etc.)

    [SerializeField]
    public int damage;

    private bool isMoving;

    private void Update()
    {
        if (shieldUp && canUseShield && shieldCooldown <= 0 && Player.hitCount >= Player.maxHitCount)
        {
            canUseShield = false;
            shieldCooldown = timeShieldCooldown;
        }

        if (shieldCooldown > 0)
        {
            shieldCooldown -= Time.deltaTime;
        }
        else if (canUseShield == false)
        {
            canUseShield = true;
            Player.hitCount = 0;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            isMoving = true;
        else
            isMoving = false;

        if (Input.GetKey(KeyCode.Mouse1) && movement.isGrounded && canUseShield)
        {
            shieldUp = true;
            animator.SetBool("isBlocking", true);
        }
        else
        {
            animator.SetBool("isBlocking", false);
            shieldUp = false;
        }

        if (timeBtwAttack < -0.0001)
        {

            //On peut attaquer
            if (Input.GetKey(KeyCode.Mouse0) && movement.isGrounded && !shieldUp && !isMoving)
            {
                animator.SetTrigger("isAttacking"); //créé par moi-même
                                                    //Crée un cercle à une position et rayon définie et tous les ennemis
                                                    //se trouvant dans cette 

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].tag == "projectile")
                    {
                        enemiesToDamage[i].GetComponent<projectileScript>().destroyProj();
                    }
                    else if (enemiesToDamage[i].tag == "ennemy")
                    {
                        enemiesToDamage[i].GetComponent<ennemy>().TakeDamage(damage);
                    }



                }
                timeBtwAttack = AttackCooldown;
            }
            else
            {

            }


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
