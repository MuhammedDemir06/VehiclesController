using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInputManager : MonoBehaviour
{
    public static VehicleInputManager Instance;

    public float InputX, InputZ;//For now
    private void OnEnable()
    {
        VehicleGearSystem.Drive += DriveControl;
    }
    private void OnDisable()
    {
        VehicleGearSystem.Drive -= DriveControl;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void GetInput()
    {
        InputX = Input.GetAxis("Horizontal"); 
    }
    private void DriveControl(float inputZ)
    {
        if (Input.GetKey(KeyCode.W))
        {
            InputZ = inputZ;
        }
        else if (Input.GetKeyUp(KeyCode.W) || InputZ == 0)
            InputZ = 0;
    }
    private void Update()
    {
        GetInput();
    }
}
