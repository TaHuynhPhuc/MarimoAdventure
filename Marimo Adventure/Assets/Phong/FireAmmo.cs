using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAmmo : MonoBehaviour
{
    public float speed = 10f; // Tốc độ của mũi tên
    private Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu mũi tên chạm đất
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
        // Update is called once per frame
        void Update()
        {

        }
    }
}
