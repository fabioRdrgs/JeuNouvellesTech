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

    // Start is called before the first frame update
    void Start()
    {
        speed = speedOrigin;
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
            speed = 0;
            stunTime -= Time.deltaTime;
        }

    }
    protected void Shootprojectile(Vector2 vector)
    {

        Transform  projectileInst = Instantiate(projectile, transform.position, transform.rotation);
        GameObject goP = GameObject.Find(projectileInst.name);
        projectileScript proj = goP.GetComponent<projectileScript>();
        if (vector == new Vector2(2, 2))
        {
            proj.setMove(2);
        }
        else
        {
            proj.setMove(-2);
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
