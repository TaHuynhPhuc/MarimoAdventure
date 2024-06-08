using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAttack : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>() != null)
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
