using UnityEngine;
using TMPro;
public class SceneTrungController : MonoBehaviour
{
    public TMP_Text textP1;
    public TMP_Text textP2;

    void Start()
    {
        textP1.text = PlayerPrefs.GetString("Player1Name", "P1");
        textP2.text = PlayerPrefs.GetString("Player2Name", "P2");
    }
}
