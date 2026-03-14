using UnityEngine;
using TMPro;

public class Container : MonoBehaviour
{
    public ShapeType acceptedType;
    public TextMeshProUGUI counterText;
    public int maxCount = 5;

    [HideInInspector] public int currentCount = 0;

    // DODAJ TO: Wywo³uje siê raz na starcie gry
    private void Start()
    {
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();

        if (item != null && item.shapeType == acceptedType && currentCount < maxCount)
        {
            currentCount++;
            UpdateUI(); // Odœwie¿a UI po zebraniu
            Destroy(item.gameObject);

            GameManager.Instance.CheckWinCondition();
        }
    }

    void UpdateUI()
    {
        if (counterText != null)
        {
            // Obliczamy ile pozosta³o:
            int remaining = maxCount - currentCount;
            counterText.text = acceptedType.ToString() + ": " + currentCount + "/" + maxCount;
        }
    }
}