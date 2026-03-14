using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Tu przeci¹gnij Panel Menu
    private bool isPaused = false;

    void Start()
    {
        // Chowa menu na samym pocz¹tku gry
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Blokowanie kursora (dla gier FPP/TPP)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Odblokowanie kursora, ¿eby móc klikaæ przyciski
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // RESET CZASU PRZED ZMIAN¥ SCENY
        SceneManager.LoadScene("MenuUI");
    }
}