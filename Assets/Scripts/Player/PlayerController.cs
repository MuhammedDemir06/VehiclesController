using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField] private float speed;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float mass = 8f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float jumpPower = 20;
    public bool IsGrounded;
    private Vector3 velocity;
    private CharacterController cc;
    private void OnEnable()
    {
        PlayerInputManager.PlayerMoveInput += Move;
        PlayerInputManager.PlayerJump += Jump;
    }
    private void OnDisable()
    {
        PlayerInputManager.PlayerMoveInput -= Move;
        PlayerInputManager.PlayerJump -= Jump;
    }
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Move(Vector3 moveInput)
    {
        IsGrounded = Physics.CheckSphere(groundCheckTransform.position, groundDistance, groundLayer);

        if (IsGrounded && velocity.y < 0)
            velocity.y = -2f;

        //Move
        var move = transform.right * moveInput.x + transform.forward * moveInput.z;
        cc.Move(move * speed * Time.deltaTime);

        //Gravitiy
        velocity.y += gravity * mass * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
    private void Jump()
    {
        if (IsGrounded)
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
    }
}