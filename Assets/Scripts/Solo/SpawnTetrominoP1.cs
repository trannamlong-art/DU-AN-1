using UnityEngine;

[DisallowMultipleComponent]
public class SpawnTetrominoP1 : MonoBehaviour
{
    [Tooltip("Prefabs của tất cả 7 khối tetromino")]
    public GameObject[] tetrominoPrefabs;

    private const int boardWidth = 10;
    private const int boardHeight = 20;

    private int lastTetromino = -1;
    private int secondLastTetromino = -1;

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (tetrominoPrefabs == null || tetrominoPrefabs.Length == 0)
        {
            Debug.LogError("SpawnTetrominoP1: Chưa gán 'tetrominoPrefabs'.");
            return;
        }

        int spawnGridX = boardWidth / 2 - 1;  // 4
        int spawnGridY = boardHeight - 1;     // 19
        Vector3 worldPos = transform.position + new Vector3(spawnGridX, spawnGridY, 0f);

        int index = GetRandomTetrominoIndex();

        GameObject newTetromino = Instantiate(
            tetrominoPrefabs[index],
            worldPos,
            Quaternion.identity
        );

        TetrisBlock tb = newTetromino.GetComponent<TetrisBlock>();
        if (tb != null)
            tb.SendMessage("SnapToGrid");
    }

    private int GetRandomTetrominoIndex()
    {
        int index;
        int maxAttempts = 10;
        int attempts = 0;

        do
        {
            index = Random.Range(0, tetrominoPrefabs.Length);
            attempts++;
        } while ((index == lastTetromino && index == secondLastTetromino) && attempts < maxAttempts);

        secondLastTetromino = lastTetromino;
        lastTetromino = index;

        return index;
    }
}
