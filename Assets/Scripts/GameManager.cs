using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public float fallSpeed = 1.0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateFallSpeed();
        Debug.Log($"[Score] +{amount} điểm → Tổng điểm: {score} | Tốc độ rơi: {fallSpeed:0.0}s");
    }


    private void UpdateFallSpeed()
    {
        if (score >= 2250) fallSpeed = 0.1f;
        else if (score >= 2000) fallSpeed = 0.2f;
        else if (score >= 1750) fallSpeed = 0.3f;
        else if (score >= 1500) fallSpeed = 0.4f;
        else if (score >= 1250) fallSpeed = 0.5f;
        else if (score >= 1000) fallSpeed = 0.6f;
        else if (score >= 750) fallSpeed = 0.7f;
        else if (score >= 500) fallSpeed = 0.8f;
        else if (score >= 250) fallSpeed = 0.9f;
        else fallSpeed = 1.0f;
    }
}
