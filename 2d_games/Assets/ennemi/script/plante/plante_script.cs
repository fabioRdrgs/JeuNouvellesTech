using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class plante_script : ennemy
{
    [SerializeField]
    private GameObject instancePlante;

    bool detect_player = false;
    bool alive = true;
    bool shoot = false;

    Stopwatch stopwatch = new Stopwatch();

    void Start()
    {
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        disttoPlayer = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("distance_player", disttoPlayer);
        animator.SetBool("detect_player", detect_player);
        animator.SetFloat("hp", gameHandler.healthValue);
        animator.SetBool("death", !alive);

        if (stopwatch.ElapsedMilliseconds >= 2000 && disttoPlayer > 3.5)
        {
            shoot = true;
        }

        if (disttoPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasePlayer();
        }

        if (gameHandler.healthValue <= 0.05)
        {
            alive = false;
            if (animator.GetNextAnimatorStateInfo(0).IsName("plante_death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 2)
            {
                Destroy(instancePlante);
            }
        }
    }

    private void StopChasePlayer()
    {
        detect_player = false;
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        if (shoot)
        {
            detect_player = true;
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
