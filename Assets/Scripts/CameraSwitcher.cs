using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPersonCam;
    public CinemachineVirtualCamera firstPersonCam;

    private bool isFirstPerson = false;

    void Start()
    {
        SetThirdPerson();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;

            if (isFirstPerson)
                SetFirstPerson();
            else
                SetThirdPerson();
        }
    }

    void SetFirstPerson()
    {
        firstPersonCam.Priority = 20;
        thirdPersonCam.Priority = 10;
    }

    void SetThirdPerson()
    {
        thirdPersonCam.Priority = 20;
        firstPersonCam.Priority = 10;
    }
}