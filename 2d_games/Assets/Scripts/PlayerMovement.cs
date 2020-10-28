using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;
    public float jumpForce;
    public float checkRadius;
    public Transform feetPos;
    public LayerMask whatIsGround;
    public float jumpTime;

    private float jumpTimeCounter;
    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool isJumping;

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

        if (moveInput > 0) {
            transform.eulerAngles = new Vector3 (0, 0, 0);
        } else if (moveInput < 0) {
            transform.eulerAngles = new Vector3 (0, 180, 0);
        }

        if (Input.GetKeyDown (KeyCode.Space) && isGrounded == true) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey (KeyCode.Space) && isJumping == true) {
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp (KeyCode.Space)) {
            isJumping = false;
        }
    }
}