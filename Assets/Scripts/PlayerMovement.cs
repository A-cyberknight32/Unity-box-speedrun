using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform cam;

    [Header("Movement")]
    public float speed = 6.0f;
    public float gravity = -19.62f;
    public float jumpHeight = 2.0f;
    public float rotationSpeed = 10.0f;

    [Header("Camera")]
    public Transform firstPersonTarget;
    public Transform thirdPersonTarget;
    public float cameraSmoothSpeed = 10f;

    private bool isFirstPerson = false;

    private Vector3 velocity;
    private bool isGrounded;
    private float rotationVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Start w trzeciej osobie
        cam.position = thirdPersonTarget.position;
        cam.rotation = thirdPersonTarget.rotation;
    }

    void Update()
    {
        HandleCameraSwitch();
        HandleMovement();
        ApplyGravity();
    }

    void LateUpdate()
    {
        UpdateCameraPosition();
    }

    void HandleCameraSwitch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;
        }
    }

    void UpdateCameraPosition()
    {
        Transform target = isFirstPerson ? firstPersonTarget : thirdPersonTarget;

        cam.position = Vector3.Lerp(
            cam.position,
            target.position,
            cameraSmoothSpeed * Time.deltaTime
        );

        cam.rotation = Quaternion.Lerp(
            cam.rotation,
            target.rotation,
            cameraSmoothSpeed * Time.deltaTime
        );
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

