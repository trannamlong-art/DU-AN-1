using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    /* =======  CONFIG  ======= */
    public Vector3 rotationPoint = new Vector3(0.5f, 0.5f, 0f);
    public float fallTime = 0.8f;

    public const int width = 10;   // cột 0‑9
    public const int height = 20;   // 20 hàng hiển thị (0‑19)
    private const int hiddenRows = 4;    // hàng spawn/ẩn
    private const int gridHeight = height + hiddenRows; // =24
    private const int bottomOffset = 9;   // world‑y của hàng 0 → -9

    /* =======  RUNTIME  ======= */
    private float previousTime;
    private static readonly Transform[,] grid = new Transform[width, gridHeight];

    /* =======  UNITY HOOKS  ======= */

    private void OnEnable()
    {
        previousTime = Time.time;

        // Game Over nếu spawn đã đè khối cũ
        if (!ValidMove())
        {
            Debug.Log("Game Over");
            enabled = false;
            // TODO: gọi GameManager.GameOver() nếu có
        }
    }

    void Update()
    {
        HandleInput();
        HandleFalling();
    }

    /* =======  INPUT  ======= */

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Move(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) Move(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.UpArrow)) Rotate();
    }

    /* =======  FALLING  ======= */

    private void HandleFalling()
    {
        float step = Input.GetKey(KeyCode.DownArrow) ? fallTime / 10f : fallTime;

        if (Time.time - previousTime > step)
        {
            transform.position += Vector3.down;
            SnapToGrid();

            if (!ValidMove())
            {
                transform.position += Vector3.up;
                SnapToGrid();
                AddToGrid();
                enabled = false;
                FindFirstObjectByType<SpawnTetromino>().SpawnNext();
            }

            previousTime = Time.time;
        }
    }

    /* =======  MOVE & ROTATE  ======= */

    private void Move(Vector3 dir)
    {
        transform.position += dir;
        SnapToGrid();
        if (!ValidMove()) transform.position -= dir;
    }

    private void Rotate()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint),
                               Vector3.forward, 90);
        SnapToGrid();
        if (!ValidMove())
            transform.RotateAround(transform.TransformPoint(rotationPoint),
                                   Vector3.forward, -90);
    }

    /* =======  GRID HELPERS  ======= */

    private void SnapToGrid()
    {
        foreach (Transform child in transform)
        {
            Vector3 localPos = child.localPosition;
            child.localPosition = new Vector3(Mathf.Round(localPos.x), Mathf.Round(localPos.y), 0f);
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0f);
    }


    private Vector2Int GridPos(Transform t)
    {
        int x = Mathf.RoundToInt(t.position.x);
        int y = Mathf.RoundToInt(t.position.y) + bottomOffset;
        return new Vector2Int(x, y);          // (0‒9, 0‒23)
    }

    /* -- ghi block vào lưới ------------------------------------------------ */

    private void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            Vector2Int pos = GridPos(child);
            if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= gridHeight)
                continue;

            grid[pos.x, pos.y] = child;
            child.SetParent(null);            // block trở thành gạch tĩnh
        }
    }

    /* -- kiểm tra nước đi hợp lệ ------------------------------------------ */

    private bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            // dưới đáy hoặc ra mép
            if (x < 0 || x >= width || y < -bottomOffset) return false;

            int gY = y + bottomOffset;
            if (gY >= gridHeight) continue;   // vượt hàng ẩn → OK

            // ô đã có gạch khác?
            Transform t = grid[x, gY];
            if (t != null && t.parent != transform) return false;
        }
        return true;
    }
    private void RowDown(int startY)
    {
        for (int y = startY; y < gridHeight; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }
}
    