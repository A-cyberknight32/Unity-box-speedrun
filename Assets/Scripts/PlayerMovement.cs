using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform cam; // Referencja do g³ównej kamery

    [Header("Ustawienia Ruchu")]
    public float speed = 6.0f;
    public float gravity = -19.62f;
    public float jumpHeight = 2.0f;
    public float rotationSpeed = 10.0f;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform; // Automatycznie znajduje g³ówn¹ kamerê

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal"); // GetAxisRaw daje lepsz¹ responsywnoœæ
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // 1. Obliczamy k¹t obrotu kamery wzglêdem osi Y
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // 2. P³ynnie obracamy postaæ do tego k¹ta
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // 3. Obliczamy kierunek ruchu w stronê, w któr¹ obrócona jest kamera
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private float rotationVelocity; // Zmienna pomocnicza do SmoothDampAngle
}
