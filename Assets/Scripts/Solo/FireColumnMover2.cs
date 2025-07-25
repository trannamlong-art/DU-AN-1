using UnityEngine;

public class FireColumnMover2 : MonoBehaviour
{
    public float speed = 10f;
    private const float leftLimit = 11f; // Giới hạn trái mới

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < leftLimit)
        {
            Destroy(gameObject);
        }
    }
}
