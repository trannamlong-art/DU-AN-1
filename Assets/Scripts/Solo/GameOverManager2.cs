using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager2 : MonoBehaviour
{
    [Header("Main Panels")]
    public GameObject gameOverUI;
    public GameObject p1WinUI;
    public GameObject p2WinUI;
    public GameObject drawUI;

    [Header("Score Display")]
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    [Header("Buttons")]
    public Button restartButton;
    public Button menuButton;

    [Header("Winner UI")]
    public TextMeshProUGUI winText;

    [Header("Audio")]
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    private string player1Name = "P1";
    private string player2Name = "P2";

    private void Start()
    {
        // Lấy tên từ PlayerPrefs (hoặc mặc định)
        player1Name = PlayerPrefs.GetString("Player1Name", "P1");
        player2Name = PlayerPrefs.GetString("Player2Name", "P2");

        // Ẩn toàn bộ UI GameOver lúc bắt đầu
        if (gameOverUI) gameOverUI.SetActive(false);
        if (p1WinUI) p1WinUI.SetActive(false);
        if (p2WinUI) p2WinUI.SetActive(false);
        if (drawUI) drawUI.SetActive(false);

        // Gán sự kiện cho nút
        if (restartButton) restartButton.onClick.AddListener(RestartGame);
        if (menuButton) menuButton.onClick.AddListener(ReturnToMenu);

        // Tạo AudioSource nếu chưa có
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void ShowGameOver(int scoreP1, int scoreP2)
    {
        Time.timeScale = 0f;

        if (gameOverUI) gameOverUI.SetActive(true);

        if (p1ScoreText) p1ScoreText.text = $"{player1Name}: {scoreP1}";
        if (p2ScoreText) p2ScoreText.text = $"{player2Name}: {scoreP2}";

        if (scoreP1 > scoreP2)
        {
            if (p1WinUI) p1WinUI.SetActive(true);
            if (winText) winText.text = $"{player1Name} Wins!";
        }
        else if (scoreP2 > scoreP1)
        {
            if (p2WinUI) p2WinUI.SetActive(true);
            if (winText) winText.text = $"{player2Name} Wins!";
        }
        else
        {
            if (drawUI) drawUI.SetActive(true);
            if (winText) winText.text = "It's a Draw!";
        }

        Debug.Log($"GAME OVER – {player1Name}: {scoreP1} | {player2Name}: {scoreP2}");
    }

    public void RestartGame()
    {
        PlayClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        PlayClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    private void PlayClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
