using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class plante_script : ennemy
{
    [SerializeField]
    private GameObject instancePlante;

    public float agroRange;

    private bool right = true;
    bool alive = true;
    bool shoot = true;
    int compteur = 0;

    Stopwatch stopwatch = new Stopwatch();

    // Update is called once per frame
    void Update()
    {
        disttoPlayer = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("distance_player", disttoPlayer);
        animator.SetFloat("hp", gameHandler.healthValue);
        animator.SetBool("death", !alive);

        if (compteur!=700)
        {
            compteur++;
        }
        else
        {
            shoot = true;
            compteur = 0;
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
        animator.SetBool("detect_player", false);
        right = false;
        shoot = true;
    }

    private void ChasePlayer()
    {
        right = true;
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(7, 7);
        }
        else
        {
            transform.localScale = new Vector2(-7, 7);
        }

        if (right)
        {
            if (shoot)
            {
                animator.SetBool("detect_player", true);
                Shootprojectile(transform.localScale);
                stopwatch.Reset();
                shoot = false;
            }
            animator.SetBool("detect_player", false);
        }
    }
}
