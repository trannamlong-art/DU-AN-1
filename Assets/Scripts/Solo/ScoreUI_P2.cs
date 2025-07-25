using UnityEngine;
using TMPro;

public class ScoreUI_P2 : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameManagerP2.Instance != null)
        {
            scoreText.text = "Score: " + GameManagerP2.Instance.score.ToString();
        }
    }
}
