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
    //public GameOverScreen GameOverScreen;
    //int maxPlatform = 0;
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

        /*if (havemove && feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //huong cua player
            int huong = (int)transform.localScale.x;
            //lay rotate cua dust
            Quaternion rotatedust = dust.transform.localRotation;
            if (huong == 1)
                rotatedust.y = 180;
            else if (huong == -1)
                rotatedust.y = 0;
            dust.transform.localRotation = rotatedust;//cap nhat
            dust.Play();
        }*/
    }

    /*public void OnFire(InputAction.CallbackContext context)
    {
        //Quaternion arrowRotation = playerMovement.isFacingRight ? Quaternion.indentity : Quaternion.Euler(0, 180f, 0); 
        Instantiate(arrowObj, bowPosition.position, Quaternion.identity );
    }*/

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
        if (isAlive == false)
        {
            return;
        }
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            rig.velocity += new Vector2(0f, jumpspeed);
        }
    }

    void Climlander()
    {
        if (isAlive == false)
        {
            return;
        }
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rig.gravityScale = startgravityscale;
            return;
        }

        rig.velocity = new Vector2(rig.velocity.x, moveInput.y * climbspeed);
        rig.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("topenemy"))
        {
            string name = collision.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
            FindObjectOfType<GameSession>().AddScore(10);
        }
        else if (collision.gameObject.CompareTag("bodyenemy"))
        {
            Die();
        }

    }

    void Die()
    {
        isAlive = false;
        anim.SetTrigger("Die");
        FindObjectOfType<GameSession>().PlayerDeath();
        //GameOver();
    }
    /*public void GameOver()
    {
        GameOverScreen.Setup(maxPlatform);
    }*/
}
