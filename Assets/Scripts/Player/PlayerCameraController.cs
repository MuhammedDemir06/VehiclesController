using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Player Camera")]
    [SerializeField] private float mouseSens = 100f;
    [SerializeField] private float cameraMinAngle=-80f, cameraMaxAngle = 80f;
    [SerializeField] private Transform player;
    private float rotationX;
    private float mouseX, mouseY;
    private void Start()
    {
        transform.parent = player;
    }
    private void GetInput()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
    }
    private void CameraMove()
    {
        rotationX -= mouseY;
        player.Rotate(Vector3.up, mouseX);
        rotationX = Mathf.Clamp(rotationX, cameraMinAngle, cameraMaxAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
    private void Update()
    {
        GetInput();
    }
    private void LateUpdate()
    {
        CameraMove();
    }
}
