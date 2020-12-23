using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class plante_script : ennemy
{
    [SerializeField]
    private GameObject instancePlante;
    [SerializeField]
    EnemyAttack combat;
    bool detect_player = false;
    bool alive = true;
    bool shoot = false;

    Stopwatch stopwatch = new Stopwatch();

    void Start()
    {
        player = GameObject.Find("Sprite").GetComponent<Transform>();
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // calcul la distance entre le monstre et le joueur
        disttoPlayer = Vector2.Distance(transform.position, player.position);

        // update les parametres de l'animator
        animator.SetFloat("distance_player", disttoPlayer);
        animator.SetBool("detect_player", detect_player);
        animator.SetFloat("hp", gameHandler.healthValue);
        animator.SetBool("death", !alive);

        // test que il y a eu plus de 2 seconde entre chaque tirs et que la distance avec le joueur est plus grande que 3.5 
        if (stopwatch.ElapsedMilliseconds >= 2000 && disttoPlayer > 3.5)
        {
            shoot = true;
        }

        // verifie que la distance avec le joueur est plus petite que la distance d'agro
        if (disttoPlayer < agroRange)
        {
            ChasePlayer();

        }
        else
        {
            StopChasePlayer();
        }

        // verifie si le monstre doit mourire
        if (gameHandler.healthValue <= 0.05)
        {
            alive = false;
            // verifie l'animation actuel et le temp qui c'est ecoulé depuis sont lancement avant de detruire le gameobject du monstre 
            if (animator.GetNextAnimatorStateInfo(0).IsName("plante_death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 2)
            {
                Destroy(instancePlante);
            }
        }
    }

    /// <summary>
    /// stop le follow du joueur
    /// </summary>
    private void StopChasePlayer()
    {
        detect_player = false;
        combat.canAttack = false;
    }

    /// <summary>
    /// script de poursuite du joueur
    /// </summary>
    private void ChasePlayer()
    {
        combat.canAttack = true;
        // rotate le monstre de facons a ce qu'il suit le joueur
        if (transform.position.x < player.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

       // verifie si il a le droit de tirer
        if (shoot)
        {
            detect_player = true;
            // verifie l'animation actuelle et le temps depuis sont activation avant de tirer un projectile 
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("plante_rangeAttack_shoot") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2)
            {
                Shootprojectile(transform, 5);
                stopwatch.Restart();
                shoot = false;
                detect_player = false;
            }
        }   
    }
}
