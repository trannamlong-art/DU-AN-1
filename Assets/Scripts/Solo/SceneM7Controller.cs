using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneM7Controller : MonoBehaviour
{
    public TMP_InputField inputP1;
    public TMP_InputField inputP2;

    public void OnPlayClicked()
    {
        PlayerPrefs.SetString("Player1Name", inputP1.text);
        PlayerPrefs.SetString("Player2Name", inputP2.text);
        SceneManager.LoadScene("SceneTrungDepzai");
    }
    public void OnBackClicked()
    {
        SceneManager.LoadScene("Menu");
    }
}
