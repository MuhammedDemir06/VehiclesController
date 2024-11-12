using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public enum Axel
{
    Front,Rear
}
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
    private bool isBrake;
    [Header("Drift")]
    [SerializeField] private bool isDrifting = false;
    [SerializeField] private float frictionMultiplier = 0.5f;

    private void UpdateDrift()
    {
        isDrifting = Input.GetKey(KeyCode.Space);

        foreach(var wheel in wheels)
        {
            if(wheel.WheelAxel==Axel.Rear)
            {
                if(isDrifting)
                {
                    if (wheel.WheelMark != null && wheel.WheelSmoke != null)
                    {
                        wheel.WheelMark.emitting = true;
                        wheel.WheelSmoke.Emit(1);
                    }
                    WheelFrictionCurve sidewaysFriction = wheel.WheelColl.sidewaysFriction;
                    sidewaysFriction.stiffness = frictionMultiplier;
                    wheel.WheelColl.sidewaysFriction = sidewaysFriction;

                    Brake();
                }
                else
                {
                    if (wheel.WheelMark != null && wheel.WheelSmoke != null)
                    {
                        wheel.WheelMark.emitting = false;
                        wheel.WheelSmoke.Emit(0);
                    }

                    WheelFrictionCurve sidewaysFriction = wheel.WheelColl.sidewaysFriction;
                    sidewaysFriction.stiffness = 1;
                    wheel.WheelColl.sidewaysFriction = sidewaysFriction;

                    if (isBrake)
                    {
                         NoBrake();
                    }
                }
            }
        }
    }
    private void HoldBrake()
    {
        isBrake = Input.GetKey(KeyCode.S);
        if (Input.GetKey(KeyCode.S))
        {
            Brake();
        }
        else
            NoBrake();
    }
    private void Brake()
    {

        foreach(var wheel in wheels)
        {
            if (wheel.WheelAxel == Axel.Front)
                wheel.WheelColl.brakeTorque = brakePower * 50000 * Time.deltaTime;
        }
    }
    private void NoBrake()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.WheelAxel == Axel.Front)
                wheel.WheelColl.brakeTorque = brakePower * 0;
        }
    }
    private void Move()
    {
        foreach(var wheel in wheels)
        {
            if(wheel.WheelAxel==Axel.Rear)
            {
                wheel.WheelColl.motorTorque = InputManager.Instance.InputZ * 1000 * accleration * Time.deltaTime;
            }
        }
    }
    private void Turn()
    {
        foreach(var wheel in wheels)
        {
            if(wheel.WheelAxel==Axel.Front)
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
        foreach(var wheel in wheels)
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
        UpdateDrift();
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