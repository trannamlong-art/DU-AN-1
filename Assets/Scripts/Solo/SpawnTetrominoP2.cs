using UnityEngine;

[DisallowMultipleComponent]
public class SpawnTetrominoP2 : MonoBehaviour
{
    [Tooltip("Prefabs của tất cả 7 khối tetromino dành cho P2")]
    public GameObject[] tetrominoPrefabs;

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
            Debug.LogError("SpawnTetrominoP2: Chưa gán 'tetrominoPrefabs'.");
            return;
        }

        float spawnX = 15.5f;
        float spawnY = 11f;
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        int index = GetRandomTetrominoIndex();

        GameObject newTetromino = Instantiate(
            tetrominoPrefabs[index],
            spawnPos,
            Quaternion.identity
        );

        TetrisBlockP2 tb = newTetromino.GetComponent<TetrisBlockP2>();
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
