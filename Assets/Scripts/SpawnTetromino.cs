using UnityEngine;

// ---------------------------------------------------------------------------
//  SpawnTetromino.cs (v3 – vị trí spawn chính xác, không double‑offset)
//  • Gắn script vào GameObject đặt ở góc trái‑dưới bảng (0,‑9)
//  • Start() sinh duy nhất 1 khối; các lần sau gọi từ TetrisBlock
// ---------------------------------------------------------------------------

[DisallowMultipleComponent]
public class SpawnTetromino : MonoBehaviour
{
    [Tooltip("Prefabs của tất cả 7 khối tetromino")]
    public GameObject[] tetrominoPrefabs;

    private const int boardWidth = 10;
    private const int boardHeight = 20;

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (tetrominoPrefabs == null || tetrominoPrefabs.Length == 0)
        {
            Debug.LogError("SpawnTetromino: Chưa gán 'tetrominoPrefabs'. Kéo 7 prefab vào Inspector.");
            return;
        }

        // Vị trí spawn: (giữa bảng, hàng trên cùng)
        int spawnGridX = boardWidth / 2 - 1;  // 4
        int spawnGridY = boardHeight - 1;     // 19

        // World pos = bottom‑left + (gridX, gridY)
        Vector3 worldPos = transform.position + new Vector3(spawnGridX, spawnGridY, 0f);

        Instantiate(
            tetrominoPrefabs[Random.Range(0, tetrominoPrefabs.Length)],
            worldPos,
            Quaternion.identity);
    }

}