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

    // Start is called before the first frame update
    void Start()
    {

    }

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
    protected void Shootprojectile(Transform transform, int speed)
    {
        Transform projectileInst = Instantiate(projectile, transform.position, transform.rotation);
        GameObject goP = GameObject.Find(projectileInst.name);
        projectileScript proj = goP.GetComponent<projectileScript>();
        if (transform.rotation.y == 180)
        {
            proj.setMove(speed);
        }
        else
        {
            proj.setMove(-speed);
        }

    }

    public void TakeDamage(int damage)
    {

        animator.SetTrigger("hurt");
        animator.SetFloat("distance_player", 10);

        stunTime = startStunTime;
        gameHandler.Damage(damage);


    }
}
