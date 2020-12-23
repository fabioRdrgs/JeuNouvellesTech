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
    private int cooldownAgroMilliseconde;
    [SerializeField]
    private GameObject instanceGolem;
    [SerializeField]
    EnemyAttack combat;
    public bool moveRight;
    bool chase = true;
    bool agro = false;
    int direction = 2;
    bool alive = true;
    
    Stopwatch stopwatch = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sprite").GetComponent<Transform>();
        stopwatch.Start();
        speedOrigin = speed;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isAlive", alive);
        // calcul la distance entre le monstre et le joueur
        disttoPlayer = Vector2.Distance(transform.position, player.position);

        // test si la distance du joueur est plus petite que la distance d'agro et le temp depuis la perte de l'agro
        if (disttoPlayer < agroRange && stopwatch.ElapsedMilliseconds >= cooldownAgroMilliseconde)
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
        // deplace le monstre dans la direction voulue
        transform.Translate(direction * Time.deltaTime * speed, 0, 0);
        // verifie si le monstre doit mourire
        if (gameHandler.healthValue <= 0.05)
        {
            alive = false;
            // verifie l'animation actuel et le temp qui c'est ecoulé depuis sont lancement avant de detruire le gameobject du monstre 
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("golem_death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5)
            {
                Destroy(instanceGolem);
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
            // verifie la position x du monstre en fonction de la position x du joueur de facons a rotate le sprite du monstre dans la direction du joueur
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
    /// permet au golem de se retourné quand il arrivent d'un coté ou de l'autre de sa zone
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
    /// 
    /// </summary>
    /// <param name="trig">le collider box de n'import quelle objet qui touche le box collider du golem</param>
    private void OnTriggerEnter2D(Collider2D trig)
    {
        // verifie si le golem touche un collider box dont le tag du gameObject est "turn" 
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
