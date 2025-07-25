using UnityEngine;

public class FireColumnMover : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Tự hủy sau khi chạy khỏi màn hình
        if (transform.position.x > 10f) // x > 10 là ngoài lưới
        {
            Destroy(gameObject);
        }
    }
}
    