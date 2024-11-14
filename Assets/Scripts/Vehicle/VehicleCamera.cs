using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
    [Header("Vehcile Cameras")]
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private int cameraIndex;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void ChangeCamera()
    {
        if (cameraIndex < cameras.Length) 
        {
            cameraIndex++;

            if (cameras[cameraIndex - 1] != null)
            {
                cameras[cameraIndex - 1].SetActive(false);
            }

            if (cameraIndex == cameras.Length)
            {
                cameraIndex = 0;
            }
        }
        cameras[cameraIndex].SetActive(true);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCamera();
        }
    }
}
