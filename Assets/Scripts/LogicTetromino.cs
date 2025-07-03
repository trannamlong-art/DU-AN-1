using UnityEngine;

public class LogicTetromino : MonoBehaviour
{
    public float falldownSpeed = 1.0f;
    public float rotationAngle = 90f;

    private bool isActive = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    void Update()
    {
        if (!isActive) return;

        // Rơi xuống
        transform.position += Vector3.down * falldownSpeed * Time.deltaTime;

        // Xoay Tetromino
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
        {
            transform.Rotate(0, 0, rotationAngle);
        }

        // Di chuyển trái/phải nếu được phép
        if (Input.GetKey(KeyCode.LeftArrow) && canMoveLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * 5f;
        }
        if (Input.GetKey(KeyCode.RightArrow) && canMoveRight)
        {
            transform.position += Vector3.right * Time.deltaTime * 5f;
        }
    }

    // Va chạm với tường (isTrigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WallLeft"))
        {
            canMoveLeft = false;
        }
        else if (other.CompareTag("WallRight"))
        {
            canMoveRight = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WallLeft"))
        {
            canMoveLeft = true;
        }
        else if (other.CompareTag("WallRight"))
        {
            canMoveRight = true;
        }
    }

    // Va chạm với đáy (không isTrigger)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bottom"))
        {
            isActive = false;
        }
    }
}