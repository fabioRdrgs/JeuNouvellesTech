using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class golemScript : ennemy
{
    [SerializeField]
    EnemyAttack combat;
    public bool moveRight;
    bool chase = true;
    bool agro = false;
    int direction = 2;
    bool alive = true;
    
    Stopwatch stopwatch = new Stopwatch();
    /// <summary>
    /// Appelle la méthode avant la première image par seconde
    /// </summary>
    void Start()
    {
        stopwatch.Start();
        speedOrigin = speed;
    }

    /// <summary>
    /// Appelle cette méthode à chaque image par secondes de l'application
    /// </summary>
    void Update()
    {
        //Défini le paramètre est vivant à vivant
        animator.SetBool("isAlive", alive);
        // calcule la distance entre le monstre et le joueur
        disttoPlayer = Vector2.Distance(transform.position, player.position);

        // test si la distance du joueur est plus petite que la distance d'agro et le temp depuis la perte de l'agro
        if (disttoPlayer < agroRange && stopwatch.ElapsedMilliseconds >= 3000)
        {
            ChasePlayer();
        }
        else
        {
            if (chase == false)
            {
                stopwatch.Restart();
                StopChasePlayer();
            }
            Move();
        }
        // déplace le monstre dans la direction voulue
        transform.Translate(direction * Time.deltaTime * speed, 0, 0);
        // vérifie si le monstre doit mourire
        if (gameHandler.healthValue <= 0.05)
        {
            alive = false;
            // verifie l'animation actuel et le temp qui c'est ecoulé depuis sont lancement avant de detruire le gameobject du monstre 
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("golem_death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5)
            {
                Destroy(GameObject.Find(transform.name));
            }
        }
    }

    /// <summary>
    /// stop la poursuite du joueur
    /// </summary>
    private void StopChasePlayer()
    {
        speed = speedOrigin;
        agro = false;
        chase = true;
        combat.canAttack = false;
    }


    /// <summary>
    /// permet au monstre de suivre le joueur
    /// </summary>
    private void ChasePlayer()
    {
        animator.SetBool("inMovement", true);
        if (chase)
        {
            combat.canAttack = true;
            speed += speedOrigin + 1;
            chase = false;
            agro = true;
        }
        if (agro)
        {
            // vérifie la position x du monstre en fonction de la position x du joueur de facons a rotate le sprite du monstre dans la direction du joueur
            if (transform.position.x < player.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    /// <summary>
    /// permet au golem de se retourné quand ils arrivent d'un coté ou de l'autre de sa zone
    /// </summary>
    private void Move()
    {
        animator.SetBool("inMovement", true);
        if (moveRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


    /// <summary>
    /// Lorsque le golem rentre en collision avec une autre entité possédant un collider 2D
    /// </summary>
    /// <param name="trig">le collider box l'objet qui est rentré en collision avec le golem</param>
    private void OnTriggerEnter2D(Collider2D trig)
    {
        // vérifie si le golem touche un collider box dont le tag du gameObject est "turn" 
        if (trig.gameObject.CompareTag("turn"))
        {
            // si il poursuit actuellement le joueur. il arrete de le suivre et recommence le temps avant qu'il re agro
            if (chase == false)
            {
                stopwatch.Restart();
                StopChasePlayer();
            }
            moveRight = !moveRight;
        }
    }
}
