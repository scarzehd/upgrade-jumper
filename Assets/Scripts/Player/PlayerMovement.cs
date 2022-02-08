using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public bool facingRight = true;
    public bool canTurn = true;

    public float move;
    public float jumpForce;
    public float speed;

    public bool isGrounded;
    public Transform feet;
    public float checkRadius;

    public float jumpTimeCounter;
    public float jumpTime;
    public float extraJumpTime;
    public bool isJumping;

    public LayerMask whatIsGround;

    public int currentExtraJumps;
    public int extraJumps;

    public bool isTouchingWall;
    public Transform wallCheck;
    public bool wallSliding;
    public float wallSlideSpeed;

    public int currentWallJumps;
    public int wallJumps;
    public bool wallJumping;
    public Vector2 wallJumpForce;
    public float wallJumpTime;
    public float wallJumpTimeCounter;

    public float knockback;
    public float knockbackTime;
    public float knockbackTimeCounter;
    public bool knockbackRight;


    public GameObject jumpParticle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        move = Input.GetAxisRaw("Horizontal");

        if (!wallJumping && knockbackTimeCounter <= 0)
        {
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        }
    }

    public void FloorJump(bool initial)
    {

    }

    private void Update()
    {
        if (canTurn)
        {
            if (move > 0 && facingRight == false)
            {
                Flip();
            }
            else if (move < 0 && facingRight == true)
            {
                Flip();
            }
        }

        isGrounded = Physics2D.OverlapCircle(feet.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            currentExtraJumps = extraJumps;
            currentWallJumps = wallJumps;
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            wallJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentExtraJumps > 0 && isGrounded == false && wallSliding == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            currentExtraJumps--;
            jumpTimeCounter = extraJumpTime;
            isJumping = true;
            Instantiate(jumpParticle, feet.position + new Vector3(0, 0.5f, 0), feet.rotation);
        }

        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsGround);

        if (isTouchingWall == true && isGrounded == false && move != 0 && wallJumping == false)
        {
            wallSliding = true;
            wallJumpTimeCounter = wallJumpTime;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true && currentWallJumps > 0)
        {
            wallJumping = true;
            currentWallJumps--;
            GameObject particle = Instantiate(jumpParticle, wallCheck.position + new Vector3(0, 0.5f, 0), wallCheck.rotation);
        }

        if (Input.GetKey(KeyCode.Space) && wallJumping == true)
        {
            if (wallJumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(wallJumpForce.x * -move, wallJumpForce.y);
                wallJumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                wallJumping = false;
            }
        }

        if (knockbackTimeCounter > 0)
        {
            if (knockbackRight == true)
            {
                rb.velocity = new Vector2(knockback, knockback);
            } else
            {
                rb.velocity = new Vector2(-knockback, knockback);
            }
            knockbackTimeCounter -= Time.deltaTime;
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
}
