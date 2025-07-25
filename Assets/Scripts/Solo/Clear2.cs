using UnityEngine;

public class Clear2 : MonoBehaviour
{
    public GameObject fireColumnPrefabP2;

    public void SpawnFireEffect(int y)
    {
        float worldY = y - 9f; // Giống P1
        Vector3 startPos = new Vector3(20f, worldY, 0f); // Bắt đầu từ x = 4
        Instantiate(fireColumnPrefabP2, startPos, Quaternion.identity);
    }
}
