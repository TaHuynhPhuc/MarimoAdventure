using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển của block
    public float xPoint = 5.0f;  // Tọa độ Y của điểm trên cùng
    public float yPoint = -5.0f; // Tọa độ Y của điểm dưới cùng
    public bool verticalMode;

    private Vector3 direction = Vector3.up; // Hướng di chuyển ban đầu

    void Update()
    {
        // Di chuyển block theo hướng hiện tại với tốc độ cố định
        transform.Translate(direction * speed * Time.deltaTime);

        if (verticalMode)
        {
            // Kiểm tra nếu block vượt quá điểm trên cùng
            if (transform.position.y >= xPoint)
            {
                direction = Vector3.down; // Đổi hướng di chuyển xuống
            }
            // Kiểm tra nếu block vượt quá điểm dưới cùng
            else if (transform.position.y <= yPoint)
            {
                direction = Vector3.up; // Đổi hướng di chuyển lên
            }
        }
        else
        {
            if (transform.position.x >= xPoint)
            {
                direction = Vector3.left;
            }
            else if (transform.position.x <= yPoint)
            {
                direction = Vector3.right;
            }
        }
    }
}
