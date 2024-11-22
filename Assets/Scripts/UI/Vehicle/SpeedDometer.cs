using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedDometer : MonoBehaviour
{
    [Header("UI")]
    [Header("Dometer")]
    [SerializeField] private float maxSpeed = 290f;
    [SerializeField] private RectTransform arrow;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [Header("Gear")]
    [SerializeField] private TextMeshProUGUI gearText;
    private void OnEnable()
    {
        VehicleGearSystem.Drive += Gear;
    }
    private void OnDisable()
    {
        VehicleGearSystem.Drive -= Gear;
    }
    private void Gear(float currentGear)
    {
        if (currentGear == 1)
            gearText.text = "D";
        else if (currentGear == 0)
            gearText.text = "N";
        else if (currentGear == -1)
            gearText.text = "R";
    }
    private void Dometer()
    {
        arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minAngle, maxAngle, VehicleSpeedDometer.Instance.Speed / maxSpeed));
    }
    private void Update()
    {
        Dometer();
    }
}
