using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public bool isJumping;
    public bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rigidBody;
    public Animator animator;

    private Vector3 velocity = Vector3.zero;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        
        // en cas de saut, le prendre en compte dans le mouvement
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        // calcul de la valeur du déplacement horizontal
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        MovePlayer(horizontalMovement);

        float characterVelocity = Mathf.Abs(rigidBody.velocity.x);
        animator.SetFloat("Speed", characterVelocity);

        spriteRenderer.flipX = rigidBody.velocity.x < -0.01f;
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rigidBody.velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, 0.05f);

        if (isJumping== true)
        {
            rigidBody.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
}
