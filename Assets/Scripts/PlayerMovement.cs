using UnityEngine;
using Cinemachine; // Dodaj to!

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform cam;

    [Header("Kamery Cinemachine")]
    public CinemachineVirtualCamera fppCamera;
    public CinemachineFreeLook tppCamera;
    private bool isFPP = false;

    [Header("Ustawienia Ruchu")]
    public float speed = 6.0f;
    public float gravity = -19.62f;
    public float jumpHeight = 2.0f;
    public float rotationSpeed = 10.0f;

    private Vector3 velocity;
    private bool isGrounded;
    private float rotationVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Ustawienie pocz¹tkowe kamer
        UpdateCameraPriority();
    }

    void Update()
    {
        // Prze³¹czanie kamer klawiszem C
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFPP = !isFPP;
            UpdateCameraPriority();
        }

        MovePlayer();
    }

    void MovePlayer()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (isFPP)
        {
            // --- LOGIKA FPP ---
            // Postaæ zawsze obraca siê tam, gdzie patrzy kamera (tylko oœ Y)
            transform.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);

            if (direction.magnitude >= 0.1f)
            {
                // Ruch wzglêdem kierunku patrzenia
                Vector3 moveDir = transform.right * horizontal + transform.forward * vertical;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
        else
        {
            // --- LOGIKA TPP (Twoja oryginalna) ---
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, 0.1f);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }

        // Skok i grawitacja (wspólne)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateCameraPriority()
    {
        if (isFPP)
        {
            fppCamera.Priority = 20;
            tppCamera.Priority = 10;
        }
        else
        {
            fppCamera.Priority = 10;
            tppCamera.Priority = 20;
        }
    }
}
