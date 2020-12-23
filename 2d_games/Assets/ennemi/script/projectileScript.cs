using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    [SerializeField]
   private GameObject instanceProj;
    [SerializeField]
    private int damage;
    public float speed;
    float direction;
    bool needSetMove = true;

    // Update is called once per frame
    void Update()
    {
        // deplace le projectile
        transform.Translate(direction*Time.deltaTime*speed, 0, 0);
    }

    /// <summary>
    /// obtient les box collider qui sont en contact avec le box collider du projectile 
    /// </summary>
    /// <param name="collision">box collider des objet touchés</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // verifie que le projectile ne touche pas les monstres avant de le detruire car si on ne verifie pas ca. le projectile se detruira au moment meme ou il est creer car il apparait sur l'ennemi 
        if (!collision.gameObject.CompareTag("ennemy"))
        {
            // verifie que le projectile touch le joueur
            if (collision.gameObject.CompareTag("Player"))
            {
                // recupere le script PlayerScript de l'objet touché et lui fait prendre des degats
                collision.GetComponent<PlayerScript>().TakeDamage(damage);
            }
            destroyProj();        
        }  
    }

    /// <summary>
    /// set la direction du projectile
    /// </summary>
    /// <param name="moveGet"></param>
    public void setMove(float moveGet)
    {
        if (needSetMove)
        {
            direction = moveGet;
            needSetMove = false;
        }  
    }

    /// <summary>
    /// detruit le projectile
    /// </summary>
    public void destroyProj()
    {
        Destroy(instanceProj);
    }
}
