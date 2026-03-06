using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Fox_Coins : MonoBehaviour
{
    Fox_Logic foxLogic;

    //imiê obiektu
    public string objectName;
    //czy obiekt jest zajêty?
    public bool isTaken;

    private void Start()
    {
        // Jeœli mamy gniazdo zapisu o takiej nazwie
        if (PlayerPrefs.HasKey(objectName))
        {
            // Porównywanie wartoœci w tym gnieŸdzie z 1, przechowywanie wyniku sprawdzenia w zmiennej isTaken
            // Jeœli takie gniazdo istnieje, nieuchronnie porównamy 1 z 1, co zawsze zwróci True
            isTaken = PlayerPrefs.GetInt(objectName) == 1;
            // Ustawianie stanu obiektu na W³¹czony/Wy³¹czony w zale¿noœci od wartoœci zmiennej isTaken
            gameObject.SetActive(!isTaken);
        }
        foxLogic = FindObjectOfType<Fox_Logic>();
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pobieranie liczby monet z gniazda zapisu i przechowywanie jej w tymczasowej zmiennej
            // Jeœli takie gniazdo nie istnieje, ustawiamy wartoœæ na 0
            var value = PlayerPrefs.GetInt("Coins", 0);
            // Zapisywanie zaktualizowanej liczby zebranych monet w gnieŸdzie "Coins"
            // W tym celu musimy wzi¹æ bie¿¹c¹ zmienn¹ i dodaæ do niej jedn¹
            PlayerPrefs.SetInt("Coins", value + 1);
            // Wywo³anie metody aktualizacji interfejsu u¿ytkownika (nie martw siê b³êdem; po prostu jeszcze nie napisaliœmy tej metody)
            foxLogic.GetCoin();

            // Ustawianie zmiennej
            isTaken = true;
            // Tworzenie gniazda zapisu z nazw¹ obiektu, przechowywanie w nim wartoœci "1"
            PlayerPrefs.SetInt(objectName, 1);
            // Wy³¹czanie monety
            gameObject.SetActive(false);
            //Jeœli obiekt, który dotkn¹³ monety, ma tag "Player", to...
        }
    }
}
