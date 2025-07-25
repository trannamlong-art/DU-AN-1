using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager2 : MonoBehaviour
{
    public GameObject pausePanel;
    public Slider volumeSlider;

    private void Start()
    {
        pausePanel.SetActive(false);
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void TogglePause()
    {
        bool isPaused = Time.timeScale == 0;
        Time.timeScale = isPaused ? 1 : 0;
        pausePanel.SetActive(!isPaused);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu"); // 🔁 đổi tên nếu cần
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
