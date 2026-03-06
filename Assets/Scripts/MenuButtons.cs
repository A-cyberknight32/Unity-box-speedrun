using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("Jak graæ");
    }

    public void LoadSceneByName (string SceneName)
    { 
        SceneManager.LoadScene(SceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
