using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAmmo : MonoBehaviour
{
    public float speed = 10f; // Tốc độ của mũi tên
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 5f);
         }
        
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        // Nếu mũi tên chạm kẻ thù
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Tiêu diệt kẻ thù
            Destroy(gameObject); // Tiêu diệt mũi tên
        }
    }
}
