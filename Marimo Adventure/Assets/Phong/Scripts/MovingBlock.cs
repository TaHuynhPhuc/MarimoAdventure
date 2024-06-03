using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển của block
    public float topY = 5.0f;  // Tọa độ Y của điểm trên cùng
    public float bottomY = -5.0f; // Tọa độ Y của điểm dưới cùng

    private Vector3 direction = Vector3.up; // Hướng di chuyển ban đầu

    void Update()
    {
        // Di chuyển block theo hướng hiện tại với tốc độ cố định
        transform.Translate(direction * speed * Time.deltaTime);

        // Kiểm tra nếu block vượt quá điểm trên cùng
        if (transform.position.y >= topY)
        {
            direction = Vector3.down; // Đổi hướng di chuyển xuống
        }
        // Kiểm tra nếu block vượt quá điểm dưới cùng
        else if (transform.position.y <= bottomY)
        {
            direction = Vector3.up; // Đổi hướng di chuyển lên
        }
    }
}
