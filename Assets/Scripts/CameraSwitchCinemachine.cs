using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera fppCamera;
    public CinemachineFreeLook tppCamera;

    private bool isFPP = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            ToggleCamera();
        }
    }

    void ToggleCamera()
    {
        isFPP = !isFPP;

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
