using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenUI : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI BestTimeText;
    void Start()
    {
        // Zamieniamy sekundy na format Minuty:Sekundy
        float t = GameManager.finalTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");

        resultText.text = "Gratulacje!\nTwój czas: " + minutes + ":" + seconds;


        if (PlayerPrefs.HasKey("BestTime"))
        {
            float bestTime = PlayerPrefs.GetFloat("BestTime");

            string minutes2 = ((int)bestTime / 60).ToString("00");
            string seconds2 = (bestTime % 60).ToString("00");

            BestTimeText.text = "Gratulacje!\nTwój najlepszy czas: " + minutes2 + ":" + seconds2;
        }
        else
        {
            BestTimeText.text = "Best Time: --";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Win"); 
    }
}
