using UnityEngine;
using TMPro;

public class BoxSensor : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    // ZMIANA: Typ ShapeType zamiast string, aby pasowa³ do skryptu Container
    public ShapeType targetItemShape;
    private int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Upewnij siê, ¿e szukasz skryptu 'Item' (jeœli to on ma shapeType), 
        // a nie 'Container' (który jest na pudle).
        var itemScript = other.GetComponent<Item>();

        if (itemScript != null && itemScript.shapeType == targetItemShape)
        {
            count++;
            UpdateUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var itemScript = other.GetComponent<Item>();

        if (itemScript != null && itemScript.shapeType == targetItemShape)
        {
            count--;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        // ToString() zamieni wartoœæ enuma na tekst do wyœwietlenia
        counterText.text = targetItemShape.ToString() + ": " + count;
    }
}
