using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GearType
{
    Manual, Automatic
}
public class VehicleGearSystem : MonoBehaviour
{
    public static System.Action<float> Drive;


    [Header("Gear System")]
    [Range(-1, 1)][SerializeField] private float currentGear;
    [Tooltip("Manuel gear not yet available.")][SerializeField] private GearType gearType;
    private void Start()
    {
        StartComponent();
    }
    private void StartComponent()
    {
        //Gear System
        currentGear = 0;
    }
    private void GearController()
    {
        if (gearType == GearType.Automatic)
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                if (currentGear < 1)
                {
                    currentGear++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightControl))
            {
                if (currentGear > -1)
                {
                    currentGear--;
                }
            }
        Drive?.Invoke(currentGear);
    }
    private void Update()
    {
        GearController();
    }
}
