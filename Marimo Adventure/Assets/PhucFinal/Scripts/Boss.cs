using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    public int health;
    public float attackRadius;
    public GameObject rangeAttack;
    private GameObject player;
    private float speed = 0;
    public float fixedYPosition;
    public GameObject effectAttack;
    private Animator anim;
    private bool isDelayAttack = false;
    private bool bossIsLive = true;

    public enum EnemyState
    {
        attack,
        run,
    }

    public EnemyState currentState = EnemyState.run;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(transform.position.x, fixedYPosition, transform.position.z); // Đảm bảo vị trí y ban đầu là giá trị cố định
    }

    private void Update()
    {
        // Giữ nguyên vị trí y của boss trong mỗi frame
        transform.position = new Vector3(transform.position.x, fixedYPosition, transform.position.z);

        if (bossIsLive)
        {
            switch (currentState)
            {
                case EnemyState.attack:
                    Attack();
                    break;

                case EnemyState.run:
                    Move();
                    break;
            }

            if (IsPlayerInRange() && !isDelayAttack)
            {
                SetState(EnemyState.attack);
            }
        }
    }

    IEnumerator DelayAttack()
    {
        Debug.Log("Delay");
        SetState(EnemyState.run);
        isDelayAttack = true;
        yield return new WaitForSeconds(6);
        isDelayAttack = false;
    }

    private void Attack()
    {
        StartCoroutine(DelayAttack());
        Debug.Log("Attack");
        anim.SetTrigger("Attack");
    }

    private void Move()
    {
        anim.SetTrigger("Run");
        FlipTowardsPlayer();
        if (player != null && player.activeSelf)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0; // Giữ nguyên trục y
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    private void FlipTowardsPlayer()
    {
        if (player != null && player.activeSelf)
        {
            if (transform.position.x <= player.transform.position.x)
            {
                transform.localScale = new Vector3(-4, 4, 1);
            }
            else
            {
                transform.localScale = new Vector3(4, 4, 1);
            }
        }
    }

    public bool IsPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(rangeAttack.transform.position, attackRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rangeAttack.transform.position, attackRadius);
    }

    public void BossStartRun()
    {
        speed = 1.5f;
    }

    public void BossStartIdle()
    {
        speed = 0;
    }

    public void SetState(EnemyState newState)
    {
        currentState = newState;
    }

    public void StartEffectAttack()
    {
        effectAttack.SetActive(true);
    }

    public void EndEffectAttack()
    {
        effectAttack.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("TakeDamage");
        if(health < 1)
        {
            Die();
        }
    }

    private void Die()
    {
        bossIsLive = false;
        Debug.Log("Die");
        BossStartIdle();
        anim.SetTrigger("Dead");
    }
}
