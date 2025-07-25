using UnityEngine;

public class Clear1 : MonoBehaviour
{
    public GameObject FireColumnMover1Prefab;

    public void FireColumnMover1(int y)
    {
        float worldY = y - 9; // Trừ offset
        Vector3 startPos = new Vector3(-1f, worldY, 0f); // bắt đầu ngoài lưới trái

        GameObject Fire = Instantiate(FireColumnMover1Prefab, startPos, Quaternion.identity);
        Destroy(Fire, 2f); // Tự hủy sau 2s (hoặc để script tự lo như trên)
    }
}
