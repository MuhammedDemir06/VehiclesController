using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(VehicleCamera))]
[RequireComponent(typeof(VehicleGearSystem))]
[RequireComponent(typeof(VehicleLights))]
[RequireComponent(typeof(VehicleSpeedDometer))]
public class VehicleController : MonoBehaviour
{
    [Header("Vehicle Controller")]
    [Space(10)]
    [Header("Acceleration")]
    [SerializeField] private float accleration = 20f;
    [SerializeField] private float steerAngle = 45f;
    [SerializeField] private float turnSensitivity = 1f;
    [SerializeField] private float rotationPower = 0.5f;
    [Header("Wheels")]
    [SerializeField] private List<Wheel> wheels;
    [SerializeField] private Transform steeringWheel;
    [SerializeField] private float steeringWheelTurnPower = 75f;
    [Header("Brake")]
    [SerializeField] private float brakePower = 1000f;
    [Header("Height")]
    [Range(-0.17f, 0.1f)][SerializeField] private float vehicleHeight = 0f;
    private Rigidbody rb;
    private Vector3 centerOfMass;
    private void Start()
    {
        StartComponent();
    }
    private void StartComponent()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.WheelSmoke != null)
                wheel.WheelSmoke.gameObject.SetActive(true);
            if (wheel.WheelMark != null)
                wheel.WheelMark.gameObject.SetActive(true);
        }
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }
    private void Height()
    {
        foreach(var wheel in wheels)
        {
            var newCenter = wheel.WheelColl.center;
            newCenter.y = vehicleHeight;
            wheel.WheelColl.center = newCenter;
        }
    }
    private void HoldBrake()
    {
        if (Input.GetKey(KeyCode.S))
        {
            foreach (var wheel in wheels)
            {
                wheel.WheelColl.brakeTorque = brakePower * 50000 * Time.deltaTime;

                if (wheel.WheelSmoke != null && VehicleSpeedDometer.Instance.Speed > 8)
                    wheel.WheelSmoke.Emit(1);

                if (wheel.WheelMark != null && VehicleSpeedDometer.Instance.Speed > 8)
                    wheel.WheelMark.emitting = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            foreach (var wheel in wheels)
            {
                wheel.WheelColl.brakeTorque = brakePower * 0;

                if (wheel.WheelSmoke != null)
                    wheel.WheelSmoke.Emit(0);

                if (wheel.WheelMark != null)
                    wheel.WheelMark.emitting = false;
            }
        }
    }
    private void Move()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.WheelAxel == Axel.Rear)
            {
                wheel.WheelColl.motorTorque = InputManager.Instance.InputZ * 1000 * accleration * Time.deltaTime;
            }
        }
    }
    private void Turn()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.WheelAxel == Axel.Front)
            {
                var steer = InputManager.Instance.InputX * turnSensitivity * steerAngle;
                wheel.WheelColl.steerAngle = Mathf.Lerp(wheel.WheelColl.steerAngle, steer, rotationPower);
            }
        }
        //SteeringWheel
        steeringWheel.localRotation = Quaternion.Euler(0, 0, InputManager.Instance.InputX * steeringWheelTurnPower);
    }
    private void WheelAnimate()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.WheelColl.GetWorldPose(out pos, out rot);
            wheel.WheelMesh.transform.position = pos;
            wheel.WheelMesh.transform.rotation = rot;
        }
    }
    private void Update()
    {
        WheelAnimate();
        HoldBrake();
        Height();
    }
    private void LateUpdate()
    {
        Move();
        Turn();
    }
}
[System.Serializable]
public class Wheel
{
    public GameObject WheelMesh;
    public WheelCollider WheelColl;
    public Axel WheelAxel;
    public ParticleSystem WheelSmoke;
    public TrailRenderer WheelMark;
}
[System.Serializable]
public enum Axel
{
    Front, Rear
}