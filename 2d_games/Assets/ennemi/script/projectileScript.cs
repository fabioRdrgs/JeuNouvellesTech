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
    float move;
    bool needSetMove = true;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move*Time.deltaTime*speed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("ennemy"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<PlayerScript>().TakeDamage(damage);
            }
            destroyProj();        
        }  
    }

    public void setMove(float moveGet)
    {
        if (needSetMove)
        {
            move = moveGet;
            needSetMove = false;
        }  
    }

    public void destroyProj()
    {
        Destroy(instanceProj);
    }
}
