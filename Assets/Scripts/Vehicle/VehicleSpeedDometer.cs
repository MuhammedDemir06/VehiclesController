using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VehicleSpeedDometer : MonoBehaviour
{
    public static VehicleSpeedDometer Instance;

    [Header("Speed Dometer")]
    public float Speed;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float speedBooster = 4f;
    private Rigidbody rb;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartComponent();
    }
    private void StartComponent()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void SpeedDometer()
    {
        Speed = rb.velocity.magnitude * speedBooster;
    }
    private void MaxSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    private void Update()
    {
        SpeedDometer();
        MaxSpeed();
    }
}
