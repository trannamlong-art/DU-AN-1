using UnityEngine;
using TMPro;

public class ScoreUI_P1 : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameManagerP1.Instance != null)
        {
            scoreText.text = "Score: " + GameManagerP1.Instance.score.ToString();
        }
    }
}
