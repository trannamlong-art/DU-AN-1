using UnityEngine;
using System.Collections;

public class TetrisBlockP2 : MonoBehaviour
{
    public Vector3 rotationPoint = new Vector3(0.5f, 0.5f, 0f);

    public const int width = 10;
    public const int height = 20;
    private const int hiddenRows = 4;
    private const int gridHeight = height + hiddenRows;
    private const int bottomOffset = 9;

    private const int xOffset = 11;
    private static readonly Transform[,] gridP2 = new Transform[width, gridHeight];

    private float previousTime;
    private Clear2 clearEffect;

    private void Awake()
    {
        clearEffect = FindFirstObjectByType<Clear2>();
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
                AddToGrid();
                EndGameIfNeeded();
                return;
            }

            AddToGrid();
            foreach (Transform child in transform)
            {
                Vector2Int pos = GridPos(child);
                if (pos.y >= Mathf.RoundToInt(10.5f + bottomOffset))
                {
                    EndGameIfNeeded();
                    return;
                }
            }

            enabled = false;
            FindFirstObjectByType<SpawnTetrominoP2>()?.SpawnNext();
        }
    }

    void Update()
    {
        HandleInput();
        HandleFalling();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Move(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) Move(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.UpArrow)) Rotate();
    }

    private void HandleFalling()
    {
        bool isFastFalling = Input.GetKey(KeyCode.DownArrow);
        float step = isFastFalling ? GameManagerP2.Instance.fallSpeed / 10f : GameManagerP2.Instance.fallSpeed;

        if (Time.time - previousTime > step)
        {
            transform.position += Vector3.down;
            SnapToGrid();

            if (!ValidMove())
            {
                transform.position += Vector3.up;
                SnapToGrid();
                AddToGrid();
                foreach (Transform child in transform)
                {
                    Vector2Int pos = GridPos(child);
                    if (pos.y >= gridHeight - 1) // hoặc 20 + 4 - 1
                    {
                        EndGameIfNeeded();
                        return;
                    }
                }
                enabled = false;
                StartCoroutine(CheckForLinesAndSpawn());
            }

            previousTime = Time.time;
        }
    }

    private void Move(Vector3 dir)
    {
        transform.position += dir;
        SnapToGrid();
        if (!ValidMove()) transform.position -= dir;
    }

    private void Rotate()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, 90);
        SnapToGrid();

        if (!ValidMove())
            transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, -90);
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
        int x = Mathf.RoundToInt(t.position.x) - xOffset;
        int y = Mathf.RoundToInt(t.position.y) + bottomOffset;
        return new Vector2Int(x, y);
    }

    private void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            Vector2Int pos = GridPos(child);
            if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= gridHeight) continue;
            gridP2[pos.x, pos.y] = child;
        }

        foreach (Transform child in transform) child.SetParent(null);
    }

    private bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x) - xOffset;
            int y = Mathf.RoundToInt(child.position.y);
            if (x < 0 || x >= width || y < -bottomOffset) return false;

            int gY = y + bottomOffset;
            if (gY >= gridHeight) continue;

            Transform t = gridP2[x, gY];
            if (t != null && t.parent != transform) return false;
        }
        return true;
    }

    private IEnumerator CheckForLinesAndSpawn()
    {
        yield return StartCoroutine(ClearFullLinesWithDelay());
        FindFirstObjectByType<SpawnTetrominoP2>()?.SpawnNext();
    }

    private IEnumerator ClearFullLinesWithDelay()
    {
        int linesCleared = 0;

        for (int y = 0; y < gridHeight; y++)
        {
            if (IsLineFull(y))
            {
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
            GameManagerP2.Instance.AddScore(linesCleared * 50);
        }
    }

    private bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
            if (gridP2[x, y] == null) return false;
        return true;
    }

    private void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (gridP2[x, y] != null)
            {
                Destroy(gridP2[x, y].gameObject);
                gridP2[x, y] = null;
            }
        }
    }

    private void MoveLinesDown(int fromY)
    {
        for (int y = fromY; y < gridHeight - 1; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (gridP2[x, y + 1] != null)
                {
                    gridP2[x, y] = gridP2[x, y + 1];
                    gridP2[x, y + 1] = null;
                    gridP2[x, y].position += Vector3.down;
                }
            }
        }
    }

    private void EndGameIfNeeded()
    {
        int p1Score = GameManagerP1.Instance != null ? GameManagerP1.Instance.score : 0;
        int p2Score = GameManagerP2.Instance != null ? GameManagerP2.Instance.score : 0;
        FindFirstObjectByType<GameOverManager2>()?.ShowGameOver(p1Score, p2Score);
    }
}
