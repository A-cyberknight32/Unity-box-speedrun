using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Pozwala pud³om ³atwo znaleŸæ mened¿era
    public List<Container> allContainers; // Przeci¹gnij tu swoje 3 pud³a w inspektorze
    public GameObject winScreen; // Opcjonalnie: ekran wygranej

    private void Awake()
    {
        Instance = this;
    }

    public void CheckWinCondition()
    {
        bool allFull = true;

        foreach (Container container in allContainers)
        {
            if (container.currentCount < container.maxCount)
            {
                allFull = false;
                break;
            }
        }

        if (allFull)
        {
            Debug.Log("KONIEC GRY! Wszystkie pud³a pe³ne.");
            if (winScreen != null) winScreen.SetActive(true);
            {
                Time.timeScale =0;
            }
            
        }
    }
}