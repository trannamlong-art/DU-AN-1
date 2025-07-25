using UnityEngine;

[DisallowMultipleComponent]
public class SpawnTetromino : MonoBehaviour
{
    [Tooltip("Prefabs của tất cả 7 khối tetromino")]
    public GameObject[] tetrominoPrefabs;

    [Tooltip("Script UI điều khiển các nút trái, phải, xuống, xoay")]
    public TetrisUIController uiController;  // Thêm biến tham chiếu tới UI

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

        // Tính vị trí spawn ở giữa bảng
        int spawnGridX = boardWidth / 2 - 1;
        int spawnGridY = boardHeight - 1;

        Vector3 worldPos = transform.position + new Vector3(spawnGridX, spawnGridY, 0f);

        // Sinh ra khối mới từ 1 trong 7 prefab
        GameObject newTetromino = Instantiate(
            tetrominoPrefabs[Random.Range(0, tetrominoPrefabs.Length)],
            worldPos,
            Quaternion.identity
        );

        // Lấy component TetrisBlock từ khối vừa sinh
        TetrisBlock tb = newTetromino.GetComponent<TetrisBlock>();
        if (tb != null)
        {
            // Snap vào lưới
            tb.SendMessage("SnapToGrid");

            // GÁN khối mới cho UI Controller để các nút có thể điều khiển
            if (uiController != null)
            {
                uiController.SetCurrentBlock(tb);
                Debug.Log("Gán currentBlock thành công cho UI Controller");
            }
            else
            {
                Debug.LogWarning("uiController chưa được gán trong Inspector!");
            }
        }
    }
}
