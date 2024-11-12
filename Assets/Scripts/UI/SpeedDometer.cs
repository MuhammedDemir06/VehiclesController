using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDometer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private float maxSpeed = 290f;
    [SerializeField] private RectTransform arrow;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    private void Dometer()
    {
        arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minAngle, maxAngle, VehicleSpeedDometer.Instance.Speed / maxSpeed));
    }
    private void Update()
    {
        Dometer();
    }
}
