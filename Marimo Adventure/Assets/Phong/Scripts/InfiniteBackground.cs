using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteBackground : MonoBehaviour
{
    public float speed = 2.0f;  // Tốc độ di chuyển của background
    private Vector3 startPosition;  // Vị trí ban đầu của background
    private float resetPosition;  // Vị trí cần reset

    void Start()
    {
        startPosition = transform.position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float imageWidth = spriteRenderer.bounds.size.x;  // Lấy chiều rộng của hình ảnh background
        resetPosition = startPosition.x - (imageWidth / 2);  // Tính toán vị trí cần reset
    }

    void Update()
    {
        // Di chuyển background qua trái
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Reset vị trí nếu background di chuyển qua vị trí reset
        if (transform.position.x <= resetPosition)
        {
            transform.position = startPosition;
        }
    }
}
