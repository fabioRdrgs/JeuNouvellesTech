using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject Barre;
    public GameHandler gameHandler;
    public PlayerMovement movement;
    public PlayerAttack combat;
  //  [HideInInspector]
    public int hitCount;
    [SerializeField]
    public int maxHitCount;
    [SerializeField]
    public Animator animator; 
    [HideInInspector]
    public bool alive = true;
    private float baseMoveSpeed;
    private void Start()
    {
        animator.SetBool("PlayerAlive", true);
        alive = true;
        baseMoveSpeed = movement.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();
        MoveSpeedControl();
    }
    public void DeathCheck()
    {
        //Si plus d'HP
        if (gameHandler.healthValue < 1)
        {
            animator.SetBool("PlayerAlive", false);
            movement.enabled = false;
            combat.enabled = false;
            alive = false;
            SceneManager.LoadScene(2);
        }

    }
    public void MoveSpeedControl()
    {
        if (!alive || combat.shieldUp)
        {
            movement.moveSpeed = 0;
        }
        else
        {
            movement.moveSpeed = baseMoveSpeed;
        }
    }
    public void TakeDamage(int damage)
    {

        if (combat.shieldUp && combat.canUseShield)
        {
            hitCount++;

            animator.SetTrigger("isBlockingAnAttack");
        }
        else
        {
            gameHandler.Damage(damage);
        }
    }
}
