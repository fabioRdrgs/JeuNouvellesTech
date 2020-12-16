using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class golemScript : ennemy
{

    public float agroRange;


    public bool moveRight;

    bool chase = true;

    bool right = true;

    bool agro = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = speedOrigin;
    }

    // Update is called once per frame
    void Update()
    {


        float disttoPlayer = Vector2.Distance(transform.position, player.position);
        if (agro)
        {
            if (disttoPlayer < agroRange)
            {
                ChasePlayer();
            }
            else
            {
                StopChasePlayer();
                Move();
            }
        }
        else
        {
            Move();

        }

        if (gameHandler.healthValue <= 0.05)
        {
            Destroy(GameObject.Find(transform.name));
        }
    }
    private void StopChasePlayer()
    { 
        if (chase == false)
        {
            
            right = true;
            speed = speedOrigin;
            chase = true;
            agro = false;
        }
    }

    private void ChasePlayer()
    {
        if (chase)
        {
            speed += speedOrigin + 1;
            chase = false;
        }

        if (transform.position.x < player.position.x)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(2, 2);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-2, 2);
        }

    }

    private void Move()
    {
        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(2, 2);

        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-2, 2);
        }
    }



    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (!trig.gameObject.CompareTag("Player") && !trig.gameObject.CompareTag("projectile"))
        {
            StopChasePlayer();
            moveRight = !moveRight;
        }
    }

}
