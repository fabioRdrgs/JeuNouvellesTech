using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject Barre;
    public GameHandler gameHandler;
    public PlayerMovement movement;
    public PlayerAttack combat;

    //  [HideInInspector]
    public int hitCount;
    [SerializeField]
    public int maxHitCount;
    [SerializeField]
    public Animator animator;
    [HideInInspector]
    public bool alive = true;
    private float baseMoveSpeed;
    /// <summary>
    /// Appelle la méthode avant la première image par seconde
    /// </summary>
    private void Start()
    {
        animator.SetBool("PlayerAlive", true);
        alive = true;
        baseMoveSpeed = movement.moveSpeed;
    }

    /// <summary>
    /// Appelle cette méthode à chaque image par secondes de l'application
    /// </summary>
    void Update()
    {
        //Si le joueur est mort
        if (DeathCheck())
        {
            //Si l'animation est terminée, affiche l'écran de mort
            if ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime) > 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("Player_death"))
            {
                SceneManager.LoadScene(2);
            }
        }
        MoveSpeedControl();
    }
    /// <summary>
    /// Teste si le joueur est mort
    /// </summary>
    /// <returns>Un booléen de oui ou non s'il est mort</returns>
    public bool DeathCheck()
    {
        //Si plus de point de vie on change son état de vie a false, on joue l'animation de mort et on vas a l'écran de game over.
        if (gameHandler.healthValue < 1)
        {
            animator.SetBool("PlayerAlive", false);
            movement.enabled = false;
            combat.enabled = false;
            alive = false;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Si le joueur bloque il ne peut pas se déplacer
    /// </summary>
    public void MoveSpeedControl()
    {
        if (!alive || combat.shieldUp)
        {
            movement.moveSpeed = 0;
        }
        else
        {
            movement.moveSpeed = baseMoveSpeed;
        }
    }
    /// <summary>
    /// Si le joueur bloque il ne prend pas de dégat
    /// </summary>
    /// <param name="damage">Nombre de dégats à subir</param>
    public void TakeDamage(int damage)
    {
        //Si le bouclier est levé et qu'il est utilisable, incrémente le nombre de coups subit et lance l'animation de blocage de coups
        if (combat.shieldUp && combat.canUseShield)
        {
            hitCount++;

            animator.SetTrigger("isBlockingAnAttack");
        }
        //Sinon, subit les dommages
        else
        {
            gameHandler.Damage(damage);
        }
    }
}
