using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int health = 10;
    private int damage = 1;
    private float speed = 3;

    public Transform player;

    public bool isFipped = false;
    
    public void LookforPlayer()
    {
        Vector3 fipped = transform.localScale;
        fipped.z *= -1f;
        
        if(transform.position.x > player.position.x && isFipped)
        {
            transform.localScale = fipped;
            transform.Rotate(0f, 180f, 0f);
            isFipped = false;
        }else if (transform.position.x < player.position.x && !isFipped)
        {
            transform.localScale = fipped;
            transform.Rotate(0f, 180f, 0f);
            isFipped = true;
        }
    }

    public enum EnemyState
    {
        attack,
        flying,
        death
    }

    protected EnemyState currentState = EnemyState.flying;

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.attack:
                break;

            case EnemyState.flying:
                break;

            case EnemyState.death:
                break;
        }
    }

    public void SetState(EnemyState newState)
    {
        currentState = newState;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
