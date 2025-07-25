using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;      // Kéo Pause Panel vào
    public GameObject gameOverPanel;   // Kéo Game Over Panel vào (nếu có)
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // đảm bảo game chạy bình thường khi start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
       

        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
    }
}
