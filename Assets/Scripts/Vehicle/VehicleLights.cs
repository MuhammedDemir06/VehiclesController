using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleLights : MonoBehaviour
{
    [Header("Default Lights")]
    [SerializeField] private GameObject frontLights;
    [SerializeField] private GameObject rearLights;
    [Header("Brake Lights")]
    [SerializeField] private GameObject brakeLights;
    private bool isOpenDefaultLights;
    private void OpenDefaultLights()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            isOpenDefaultLights = !isOpenDefaultLights;

            frontLights.SetActive(isOpenDefaultLights);
            rearLights.SetActive(isOpenDefaultLights);
        }
    }
    private void OpenBrakeLights()
    {
        bool isBrake = Input.GetKey(KeyCode.S);
        brakeLights.SetActive(isBrake);
    }
    private void Update()
    {
        OpenBrakeLights();
        OpenDefaultLights();
    }
}
