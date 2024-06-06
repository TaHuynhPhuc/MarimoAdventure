using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f; // Tốc độ của mũi tên
    private Rigidbody2D rb;
    public int damage = 1;
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
        // Kiểm tra nếu đối tượng va chạm có component Enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Gọi phương thức TakeDamage nếu enemy không phải là null
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
