using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Gán giá trị ban đầu
        volumeSlider.value = AudioListener.volume;

        // Lắng nghe khi slider thay đổi
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SceneLong"); // Đặt tên scene phù hợp
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
