    using UnityEngine;
    using System.Collections;

    public class TetrisBlock : MonoBehaviour
    {
        public Vector3 rotationPoint = new Vector3(0.5f, 0.5f, 0f);

        public const int width = 10;
        public const int height = 20;
        private const int hiddenRows = 4;
        private const int gridHeight = height + hiddenRows;
        private const int bottomOffset = 9;

        private float previousTime;
        private static readonly Transform[,] grid = new Transform[width, gridHeight];

        private BlockSound blockSound;
        private Clear clearEffect;

        private void Awake()
        {
            blockSound = GetComponent<BlockSound>();
            clearEffect = FindFirstObjectByType<Clear>();
        }

        private void OnEnable()
        {
            previousTime = Time.time;

            if (!ValidMove())
            {
                transform.position += Vector3.up;
                SnapToGrid();

                if (!ValidMove())
                {
                    Debug.LogError("Snap sai: vẫn invalid sau khi Snap lại");

                    // ✅ Thêm khối vào grid (dù sai)
                    AddToGrid();

                    // ✅ THÊM: Hiện UI nếu khối spawn ra đã bị kẹt
                    GameOverManager gom = FindFirstObjectByType<GameOverManager>();
                    if (gom != null)
                    {
                        gom.ShowGameOver();
                        Debug.Log("🟥 GAME OVER – Spawn ra đã bị kẹt!");
                    }
                    else
                    {
                        Debug.LogError("❌ Không tìm thấy GameOverManager!");
                    }

                    return;
                }

                AddToGrid();
                enabled = false;
                FindFirstObjectByType<SpawnTetromino>().SpawnNext();
            }
        }

        void Update()
        {
            HandleInput();
            HandleFalling();
        }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.W)) Rotate();
    }


    private void HandleFalling()
        {
        bool isFastFalling = Input.GetKey(KeyCode.S);

        float step = isFastFalling ? GameManager.Instance.fallSpeed / 10f
                                       : GameManager.Instance.fallSpeed;

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
                    StartCoroutine(CheckForLinesAndSpawn());
                }
                else if (isFastFalling)
                {
                    blockSound?.PlayDropSound(); // Rơi nhanh có hiệu lực
                }

                previousTime = Time.time;
            }
        }

        private void Move(Vector3 dir)
        {
            transform.position += dir;
            SnapToGrid();

            if (!ValidMove())
            {
                transform.position -= dir;
            }
            else
            {
                blockSound?.PlayMoveSound();
            }
        }

        private void Rotate()
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, 90);
            SnapToGrid();

            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, -90);
            }
            else
            {
                blockSound?.PlayRotateSound();
            }
        }

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
            return new Vector2Int(x, y);
        }

        private void AddToGrid()
        {
            foreach (Transform child in transform)
            {
                Vector2Int pos = GridPos(child);
                if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= gridHeight)
                    continue;

                grid[pos.x, pos.y] = child;
            }

            foreach (Transform child in transform)
            {
                child.SetParent(null);
            }
        }

        private bool ValidMove()
        {
            foreach (Transform child in transform)
            {
                int x = Mathf.RoundToInt(child.position.x);
                int y = Mathf.RoundToInt(child.position.y);

                if (x < 0 || x >= width || y < -bottomOffset) return false;

                int gY = y + bottomOffset;
                if (gY >= gridHeight) continue;

                Transform t = grid[x, gY];
                if (t != null && t.parent != transform) return false;
            }
            return true;
        }

        private IEnumerator CheckForLinesAndSpawn()
        {
            yield return StartCoroutine(ClearFullLinesWithDelay());
            FindFirstObjectByType<SpawnTetromino>().SpawnNext();
        }

        private IEnumerator ClearFullLinesWithDelay()
        {
            int linesCleared = 0;

            for (int y = 0; y < gridHeight; y++)
            {
                if (IsLineFull(y))
                {
                    blockSound?.PlayLineClearSound();
                    clearEffect?.SpawnFireEffect(y);

                    yield return new WaitForSeconds(1.0f);

                    DeleteLine(y);
                    MoveLinesDown(y);
                    y--;
                    linesCleared++;
                }
            }

            if (linesCleared > 0)
            {
                GameManager.Instance.AddScore(linesCleared * 50);
            }
        }

        private bool IsLineFull(int y)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] == null) return false;
            }
            return true;
        }

        private void DeleteLine(int y)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
            }
        }

        private void MoveLinesDown(int fromY)
        {
            for (int y = fromY; y < gridHeight - 1; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[x, y + 1] != null)
                    {
                        grid[x, y] = grid[x, y + 1];
                        grid[x, y + 1] = null;
                        grid[x, y].position += Vector3.down;
                    }
                }
            }
        }
    }

