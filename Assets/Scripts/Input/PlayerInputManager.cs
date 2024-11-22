using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static System.Action<Vector3> PlayerMoveInput;
    public static System.Action PlayerJump;
    public static System.Action PlayerWeaponReload;
    public static System.Action<bool> PlayerWeaponFire;

    [Header("Player Input")]
    [SerializeField] private Vector3 move;

    private void WeaponReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
            PlayerWeaponReload?.Invoke();
    }
    private void Fire()
    {
        var fire = Input.GetMouseButton(0);
        PlayerWeaponFire?.Invoke(fire);
    }
    private void MoveInput()
    {
        move.x = Input.GetAxis("Horizontal");
        move.z = Input.GetAxis("Vertical");

        PlayerMoveInput?.Invoke(move);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerJump?.Invoke();
    }
    private void Update()
    {
        MoveInput();
        Jump();
        Fire();
        WeaponReload();
    }
}