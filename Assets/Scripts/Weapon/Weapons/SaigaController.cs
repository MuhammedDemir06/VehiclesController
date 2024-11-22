using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaigaController : WeaponManager
{
    private void OnEnable()
    {
        PlayerInputManager.PlayerWeaponFire += SaigaFire;
        PlayerInputManager.PlayerWeaponReload += ReloadControl;
    }
    private void OnDisable()
    {
        PlayerInputManager.PlayerWeaponFire -= SaigaFire;
        PlayerInputManager.PlayerWeaponReload -= ReloadControl;
    }
    private void Start()
    {
        StartComponent();
    }
    private void ReloadControl()
    {
        Reload();
    }
    private void SaigaFire(bool fireInput)
    {
        Fire(fireInput);
        FireAnim(fireInput);
    }
}
