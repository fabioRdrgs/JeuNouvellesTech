//https://www.youtube.com/watch?v=j111eKN8sJw
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;
    private float baseMoveSpeed;
    public float jumpForce;
    public float checkRadius;
    public Transform feetPos;
    public LayerMask whatIsGround;
    public float jumpTime;

    private float jumpTimeCounter;
    private Rigidbody2D rb;
    private float moveInput;
    [HideInInspector]
    public bool isGrounded;
    private bool isJumping;

    public Animator animator;
    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> ();
    }

    //Can be called multiple times
    void FixedUpdate () {

        //On recupere la touche presser sous forme de valeur
        moveInput = Input.GetAxisRaw ("Horizontal");
        //On initialise la vélociter du personnage
        rb.velocity = new Vector2 (moveInput * moveSpeed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update () {
        //Définit la zone pour la détection du sol par les pieds
        isGrounded = Physics2D.OverlapCircle (feetPos.position, checkRadius, whatIsGround);

        //Si une des touches de déplacement est appuyé
        if (moveInput != 0) {
            //change la vitesse de l'animator pour passer d'une animation a une autre
            animator.SetFloat ("Speed", moveSpeed);
            if (moveInput > 0) {
                //Tourne l'image du personnage a droite
                transform.eulerAngles = new Vector3 (0, 0, 0);
            } else if (moveInput < 0) {
                //Tourne l'image du personnage a gauche
                transform.eulerAngles = new Vector3 (0, 180, 0);
            }
        } else {
            //On remet la variable "Speed" de l'animateur a 0
            animator.SetFloat ("Speed", 0);
        }

        //S'active quand la touche espace est maintenue
        if (Input.GetKeyDown (KeyCode.Space) && isGrounded == true) {
            isJumping = false;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;

        }

        //S'active quand la touche espace est appuyé
        if (Input.GetKey (KeyCode.Space) && isJumping == false) {
            if (jumpTimeCounter > 0) {                
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                animator.SetBool("isJumping", true);
            } else {
                isJumping = true;
            }
        }

        //Vérifie si il est au sol
        if (isGrounded == true ) {
            animator.SetBool ("isJumping", false);
            isJumping = false;
        }
    }
}