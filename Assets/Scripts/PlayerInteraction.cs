using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 30f;
    public Camera playerCamera;

    public float holdDistance = 2f;
    public float throwForce = 10f;

    private Item heldItem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldItem == null)
                TryInteract();
            else
                ThrowItem();
        }
    }

    // Trzymanie po ruchu kamery (mniej jittera)
    void LateUpdate()
    {
        if (heldItem != null)
            HoldItem();
    }

    void TryInteract()
    {
        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Debug.Log(hit.collider.gameObject.name);

            Item item = hit.collider.GetComponent<Item>();
            if (item != null)
            {
                PickUp(item);
            }
        }
    }

    void PickUp(Item item)
    {
        heldItem = item;

        // wy³¹cz fizykê
        item.rb.velocity = Vector3.zero;
        item.rb.angularVelocity = Vector3.zero;

        item.rb.useGravity = false;
        item.rb.isKinematic = true;

        // zapobiega obracaniu siê
        item.rb.freezeRotation = true;
    }

    void HoldItem()
    {
        Vector3 targetPosition =
            playerCamera.transform.position +
            playerCamera.transform.forward * holdDistance;

        // u¿ywamy fizyki zamiast teleportowania
        heldItem.rb.MovePosition(targetPosition);
    }

    void ThrowItem()
    {
        heldItem.rb.isKinematic = false;
        heldItem.rb.useGravity = true;
        heldItem.rb.freezeRotation = false;

        heldItem.rb.AddForce(
            playerCamera.transform.forward * throwForce,
            ForceMode.Impulse);

        heldItem = null;
    }
}