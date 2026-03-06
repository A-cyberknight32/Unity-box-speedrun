using UnityEngine;
using TMPro;

public class Container : MonoBehaviour
{
    public ShapeType acceptedType;
    public TextMeshProUGUI counterText;
    public int maxCount = 5;

    [HideInInspector] public int currentCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();

        if (item != null && item.shapeType == acceptedType && currentCount < maxCount)
        {
            currentCount++;
            UpdateUI();
            Destroy(item.gameObject);

            // Powiadom mened¿era, ¿eby sprawdzi³ warunek wygranej
            GameManager.Instance.CheckWinCondition();
        }
    }

    void UpdateUI()
    {
        if (counterText != null)
            counterText.text = acceptedType.ToString() + ": " + currentCount + "/" + maxCount;
    }
}