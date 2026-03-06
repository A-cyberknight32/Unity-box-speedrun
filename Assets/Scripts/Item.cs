using UnityEngine;
using UnityEngine.ProBuilder;

public class Item : MonoBehaviour
{
    public ShapeType shapeType;

    [HideInInspector]
    public Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
