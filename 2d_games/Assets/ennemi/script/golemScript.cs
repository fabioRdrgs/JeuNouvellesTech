using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class golemScript : ennemy
{
    public bool moveRight;
    bool chase = true;
    bool agro = false;
    int direction = 2;
    bool alive = true;

    Stopwatch stopwatch = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        stopwatch.Start();
        speedOrigin = speed;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isAlive", alive);
        disttoPlayer = Vector2.Distance(transform.position, player.position);
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
        transform.Translate(direction * Time.deltaTime * speed, 0, 0);

        if (gameHandler.healthValue <= 0.05)
        {
            alive = false;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("golem_death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5)
            {
                Destroy(GameObject.Find(transform.name));
            }
        }
    }
    private void StopChasePlayer()
    {
        speed = speedOrigin;
        agro = false;
        chase = true;
    }

    private void ChasePlayer()
    {
        animator.SetBool("inMovement", true);
        if (chase)
        {
            speed += speedOrigin + 1;
            chase = false;
            agro = true;
        }
        if (agro)
        {
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



    private void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.gameObject.CompareTag("turn"))
        {
            if (chase == false)
            {
                stopwatch.Restart();
                StopChasePlayer();
            }
            moveRight = !moveRight;
        }
    }
}
