using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public int heath = 3;
    int damage = 1;
    private PlayerAudio playerAudio;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        startgravityscale = rig.gravityScale;
        playerAudio = GetComponent<PlayerAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return; // Ngừng tất cả các cập nhật khi người chơi đã chết
        Run();
        Flip();
        ClimbLadder();
    }

    void Run()
    {
        if (!isAlive) return;

        rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
        bool haveMove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", haveMove);
        
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            anim.SetBool("isJumping", false);
        else
            anim.SetBool("isJumping", true);
        
    }

    void Flip()
    {
        if (!isAlive) return;

        bool haveMove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        if (haveMove)
        {
            transform.localScale = new Vector2(Mathf.Sign(rig.velocity.x), 1f);
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return; // Ngừng xử lý di chuyển khi người chơi đã chết
        moveInput = value.Get<Vector2>();
        PlayerAudio.instance.PlaySFX("run");
    }

    void OnJump(InputValue value)
    {
        if (isAlive && feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (value.isPressed)
            {
                rig.velocity += new Vector2(0f, jumpspeed);
                PlayerAudio.instance.PlaySFX("jump");
            }
        }
    }

    void ClimbLadder()
    {
        if (!isAlive) return;

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

    public void TakeDamage(int damage)
    {
        heath -= damage;
        PlayerAudio.instance.PlaySFX("Die");
        if (heath < 1)
        {
            Die();
        }
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike") || other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }*/

    void Die()
    {
        isAlive = false; // Đặt isAlive thành false khi người chơi chết
        anim.SetTrigger("Die");
        Debug.Log("Player died!");
        PlayerAudio.instance.PlaySFX("Die");
    }
}
