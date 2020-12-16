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

    void FixedUpdate () {
   
        moveInput = Input.GetAxisRaw ("Horizontal");
        rb.velocity = new Vector2 (moveInput * moveSpeed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update () {
        isGrounded = Physics2D.OverlapCircle (feetPos.position, checkRadius, whatIsGround);

        if (moveInput != 0) {
            //change la vitesse de l'animator pour passer d'une animation a une autre
            animator.SetFloat ("Speed", moveSpeed);
            if (moveInput > 0) {
                transform.eulerAngles = new Vector3 (0, 0, 0);
            } else if (moveInput < 0) {
                transform.eulerAngles = new Vector3 (0, 180, 0);
            }
        } else {
            animator.SetFloat ("Speed", 0);
        }

        //
        if (Input.GetKeyDown (KeyCode.Space) && isGrounded == true) {
            isJumping = false;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;

        }

        if (Input.GetKey (KeyCode.Space) && isJumping == false) {
            if (jumpTimeCounter > 0) {                
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                animator.SetBool("isJumping", true);
            } else {
                isJumping = true;
            }
        }

        if (isGrounded == true ) {
            animator.SetBool ("isJumping", false);
            isJumping = false;
        }
        else
        {
            
        }
    }
}