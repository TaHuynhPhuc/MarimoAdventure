using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rig;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpspeed = 5f;
    CapsuleCollider2D col;
    Animator anim;
    public BoxCollider2D feet;
    [SerializeField] float climbspeed = 10f;
    float startgravityscale;
    bool isAlive = true;
    public ParticleSystem dust;


    void Start()
    {

        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        startgravityscale = rig.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        Climlander();
        Climlander();
    }

    void Run()
    {
        if (isAlive == false)
        {
            return;
        }
        rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", havemove);

        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            anim.SetBool("isJumping", false);
        else
            anim.SetBool("isJumping", true);


    }

    void Flip()
    {
        if (isAlive == false)
        {
            return;
        }
        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;

        if (havemove)
        {
            transform.localScale = new Vector2(Mathf.Sign(rig.velocity.x), 1f);
        }
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {

        if (isAlive && feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {

            if (value.isPressed)
            {
                rig.velocity += new Vector2(0f, jumpspeed);
            }
        }
    }

    void Climlander()
    {
        if (!isAlive)
        {
            return;
        }

        if (feet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rig.gravityScale = 0;
            float verticalInput = moveInput.y;
            rig.velocity = new Vector2(rig.velocity.x, verticalInput * climbspeed);


            bool isClimbing = Mathf.Abs(rig.velocity.y) > Mathf.Epsilon;
            anim.SetBool("isClimbing", isClimbing);
        }
        else
        {
            rig.gravityScale = startgravityscale;
            anim.SetBool("isClimbing", false);
        }
    }

    
     

    void Die()
    {
        isAlive = false;
        anim.SetTrigger("Die");
        FindObjectOfType<GameSession>().PlayerDeath();
        
    }
    
}