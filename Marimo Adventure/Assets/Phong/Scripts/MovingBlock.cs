using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển của block
    public float xPoint = 5.0f;  // Tọa độ Y của điểm trên cùng
    public float yPoint = -5.0f; // Tọa độ Y của điểm dưới cùng
    public bool verticalMode;

    private Vector3 directionVer = Vector3.up; // Hướng di chuyển ban đầu
    private Vector3 directionHor = Vector3.right;

    void Update()
    {
        if (verticalMode)
        {
            // Di chuyển block theo hướng hiện tại với tốc độ cố định
            transform.Translate(directionVer * speed * Time.deltaTime);
            // Kiểm tra nếu block vượt quá điểm trên cùng
            if (transform.position.y >= xPoint)
            {
                directionVer = Vector3.down; // Đổi hướng di chuyển xuống
            }
            // Kiểm tra nếu block vượt quá điểm dưới cùng
            else if (transform.position.y <= yPoint)
            {
                directionVer = Vector3.up; // Đổi hướng di chuyển lên
            }
        }
        else
        {
            // Di chuyển block theo hướng hiện tại với tốc độ cố định
            transform.Translate(directionHor * speed * Time.deltaTime);
            if (transform.position.x >= xPoint)
            {
                directionHor = Vector3.left;
            }
            else if (transform.position.x <= yPoint)
            {
                directionHor = Vector3.right;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!verticalMode && collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!verticalMode && collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
