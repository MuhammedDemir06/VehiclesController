using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnim;
    private void OnEnable()
    {
        PlayerInputManager.PlayerMoveInput += MoveAnim;
    }
    private void OnDisable()
    {
        PlayerInputManager.PlayerMoveInput -= MoveAnim;
    }
    private void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }
    private void MoveAnim(Vector3 move)
    {
        playerAnim.SetFloat("MoveX", move.x);
        playerAnim.SetFloat("MoveY", move.z);
    }
}
