using UnityEngine;

public class Clear : MonoBehaviour
{
    public GameObject fireColumnPrefab;

    public void SpawnFireEffect(int y)
    {
        float worldY = y - 9; // Trừ offset
        Vector3 startPos = new Vector3(-1f, worldY, 0f); // bắt đầu ngoài lưới trái

        GameObject fire = Instantiate(fireColumnPrefab, startPos, Quaternion.identity);
        Destroy(fire, 2f); // Tự hủy sau 2s (hoặc để script tự lo như trên)
    }
}
