using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemy : MonoBehaviour
{
    public Transform player;
    public GameHandler gameHandler;
    public GameObject BarreDeVie;
    public Animator animator;
    public Transform projectile;
    protected float disttoPlayer;
    private float stunTime;
    public float startStunTime;
    public float speed;
    protected float speedOrigin;
    public float agroRange;

    private void Update()
    {

        //Garde sa vitesse habituelle
        if (stunTime <= 0)
        {
            speed = speedOrigin;
        }
        else
        {
            animator.SetBool("inMovement", false);
            speed = 0;
            stunTime -= Time.deltaTime;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="transform">transform du gameObject dont le projectile va partir</param>
    /// <param name="speed">vitesse a la quelle leprojectile va partir</param>
    protected void Shootprojectile(Transform transform, int speed)
    {
        // instentie la position du nouveaux projectile
        Transform projectileInst = Instantiate(projectile, transform.position, transform.rotation);
        GameObject goP = GameObject.Find(projectileInst.name);
        // recupere les composant du script projectileScript
        projectileScript proj = goP.GetComponent<projectileScript>();
        // verifie ou regarde le monstre afin de tirer le projectile dans la bonne direction 
        if (transform.rotation.y == 180)
        {
            proj.setMove(speed);
        }
        else
        {
            proj.setMove(-speed);
        }

    }

    /// <summary>
    /// fait prendre des degats au monstre
    /// </summary>
    /// <param name="damage">nombre de point de vie que le mostre va perdre </param>
    public void TakeDamage(int damage)
    {

        animator.SetTrigger("hurt");
        animator.SetFloat("distance_player", 10);

        stunTime = startStunTime;
        gameHandler.Damage(damage);


    }
}
