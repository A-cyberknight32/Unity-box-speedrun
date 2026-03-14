using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Container> allContainers;
    public string winSceneName = "Win";

    public static float finalTime;

    private float currentTime = 0f;
    private bool gameFinished = false;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;

    private float bestTime = Mathf.Infinity;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Wczytaj rekord
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
    }

    void Start()
    {
        UpdateBestTimeUI();
    }

    void Update()
    {
        if (!gameFinished)
        {
            currentTime += Time.deltaTime;

            if (timeText != null)
            {
                timeText.text = "Time: " + currentTime.ToString("F2");
            }
        }
    }

    public void CheckWinCondition()
    {
        foreach (Container c in allContainers)
        {
            if (c.currentCount < c.maxCount)
                return;
        }

        WinGame();
    }

    void WinGame()
    {
        gameFinished = true;

        finalTime = currentTime;

        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(winSceneName);
    }

    void UpdateBestTimeUI()
    {
        if (bestTimeText == null) return;

        if (bestTime == Mathf.Infinity)
            bestTimeText.text = "Best Time: --";
        else
            bestTimeText.text = "Best Time: " + bestTime.ToString("F2");
    }
}