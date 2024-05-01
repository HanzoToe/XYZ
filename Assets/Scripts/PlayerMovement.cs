using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //GameComponents
    Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallcheck;
    [SerializeField] private LayerMask wallLayer;

    //Vectors
    private Vector2 WallJumpingPower = new Vector2(4f, 8f);

    //Floats
    public float jumpingpower = 8f;
    public float slideDistance = 10f;
    public float slideTime = 0.2f;
    public float movementspeed = 3f;
    public float WallSlideSpeed = 2f;
    public float WallJumpingTime = 0.2f;
    public float wallJumpingCounter;
    public float WallJumpinDuration = 0.4f;
    float WallJumpingDirection;
    public float horizontal;

    //Bools
    private bool isfacingright = true;
    private bool canSlide = true;
    [HideInInspector] public bool isSliding;
    [HideInInspector] public bool isWallSliding;
    [HideInInspector] public bool isWalljumping;


    // Start is called before the first frame update
    void Start()
    {
        //Gets the rigidbody compenent off of the player
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire3") && canSlide && IsGrounded())
        {
            StartCoroutine("Slide");
        }

        //horizontal is a float that equals the input axis of "Horizontal" (x axis) 
        horizontal = Input.GetAxisRaw("Horizontal");

        if (!isWalljumping)
        {
            flip();
        }
        HandleJump();
        WallSlide();
        HandleWallJump();
    }

    private void FixedUpdate()
    {
        if (isSliding)
        {
            return;
        }

        if (!isWalljumping)
        {
            rb.velocity = new Vector2(horizontal * movementspeed, rb.velocity.y);
        }

        HandleWalking();
    }

    private void HandleWalking()
    {
        //Walking
        //rb.velociy is taking the linear velocity of the rigidbody allowing the character to move
        rb.velocity = new Vector2(horizontal * movementspeed, rb.velocity.y);
    }


    //An IEnumerator in the context of what you typically use it for in Unity is essentially a function that goes through each yield and returns whatever it yields as a function.
    IEnumerator Slide()
    {
        canSlide = false;
        isSliding = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * slideDistance, 0f);
        yield return new WaitForSeconds(slideTime);
        rb.gravityScale = originalGravity;
        isSliding = false;
        canSlide = true;
    }
    private void WallSlide()
    {
        if (isWalled() && !IsGrounded() && horizontal != 0f)
        {
            //Clamps the given value between the given minimum float and maximum float values.
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void HandleWallJump()
    {
        if (isWallSliding)
        {
            isWalljumping = false;
            WallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = WallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f && !IsGrounded())
        {
            isWalljumping = true;
            rb.velocity = new Vector2(WallJumpingDirection * WallJumpingPower.x, WallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != WallJumpingDirection)
            {
                isfacingright = !isfacingright;
                Vector3 Localescale = transform.localScale;
                Localescale.x *= -1f;
                transform.localScale = Localescale;
            }
        }

        Invoke(nameof(StopWallJumping), WallJumpinDuration);
    }

    private void StopWallJumping()
    {
        isWalljumping = false;
    }
    private void flip()
    {
        //It is checking if the bool "isfacingright" and what direction im moving in, to see if my character should be facing the left or right
        if (isfacingright && horizontal < 0f || !isfacingright && horizontal > 0f)
        {
            isfacingright = !isfacingright;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    private void HandleJump()
    {
        //Checks if player has inputed the "Jump" input and checks if the player is grounded
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingpower);
        }

        //Checks if the player let go of the "Jump" input mid jump to then stop them in the air, causing them to get a shorter jump.
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallcheck.position, 0.1f, wallLayer);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.1f, groundLayer);
    }
}


