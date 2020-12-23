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
    

    [SerializeField]
    public int damage;

    private bool isMoving;

    /// <summary>
    /// Appelle cette méthode à chaque image par secondes de l'application
    /// </summary>
    private void Update()
    {
        //Si le bouclier est levé, il peut être utilisé, le temps de recharge est à 0, le nombre de coups que peut prendre le joueur est plus grand ou égal au nombre max de coups
        //Désactive l'utilisation du bouclier et mets défini le temps de recharge actuel
        if (shieldUp && canUseShield && shieldCooldown <= 0 && Player.hitCount >= Player.maxHitCount)
        {
            canUseShield = false;
            shieldCooldown = timeShieldCooldown;
        }
        //Si le temps de recharge est plus grand que 0, le réduit graduellement
        if (shieldCooldown > 0)
        {
            shieldCooldown -= Time.deltaTime;
        }
        //Sinon, si le temps de recharge est plus petit que 0 ou égal et que le bouclier est inutilisable, permet de le réutiliser tout en resettant le nombre de coups infligés au joueur à 0
        else if (canUseShield == false)
        {
            canUseShield = true;
            Player.hitCount = 0;
        }

        //Si la touche A ou D est pressée, le joueur est en mouvement, sinon il ne l'est pas
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            isMoving = true;
        else
            isMoving = false;
        //Si le joueur appuie sur clic gauche, il est au sol et le bouclier est utilisable, lève le bouclier et lance l'animation de blocage
        if (Input.GetKey(KeyCode.Mouse1) && movement.isGrounded && canUseShield)
        {
            shieldUp = true;
            animator.SetBool("isBlocking", true);
        }
        //Sinon, baisse le bouclier et arrête l'animation de blocage
        else
        {
            animator.SetBool("isBlocking", false);
            shieldUp = false;
        }

        //Si le temps entre chaque attaques est inférieur à 0
        if (timeBtwAttack < -0.0001)
        {

            //On peut attaquer si le joueur appuie sur clic gauche, est au sol, s'il ne bouge pas et s'il n'a pas le bouclier levé
            if (Input.GetKey(KeyCode.Mouse0) && movement.isGrounded && !shieldUp && !isMoving)
            {
                animator.SetTrigger("isAttacking"); //Lance l'animation d'attaque

                //Crée un cercle à une position et rayon définie et tous les ennemis se trouvant dans cette zone prendront des dégats par rapport à ceux définis de base
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                //Parcoure la liste de toutes les entités se trouvant dans la zone de dégats du joueur au moment de l'attaque
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    //Si l'entité est un projectile, le détruit
                    if (enemiesToDamage[i].tag == "projectile")
                    {
                        enemiesToDamage[i].GetComponent<projectileScript>().destroyProj();
                    }
                    //Si l'entité est un ennemi, lui inflige des dégats
                    if (enemiesToDamage[i].tag == "ennemy")
                    {
                        enemiesToDamage[i].GetComponent<ennemy>().TakeDamage(damage);
                    }



                }
                //Lance le temps de recharge entre 2 attaques

                timeBtwAttack = AttackCooldown;
            }


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
