using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static System.Action<float,float> WeaponMagazineBullet;

    [Header("Weapon Controller")]
    [Space(5)]
    [Header("Features")]
    [SerializeField] protected Animator weaponAnims;
    [SerializeField] protected ParticleSystem muzzleEffect;
    [SerializeField] protected float weaponCoolDown = 2f;
    protected float weaponCoolDownTimer;
    [Header("Reload")]
    [SerializeField] protected float reloadTime, fullReloadTime;
    private float defaultReloadTime;
    [Header("Bullet")]
    public bool CanFire;
    [HideInInspector] public bool IsFire;
    [SerializeField] protected float currentBullet;
    [SerializeField] protected float magazineBullet;
    [SerializeField] protected float maxBullet;
    [SerializeField] protected float maxDistance;
    private Camera camera;
    protected void StartComponent()
    {
        camera = Camera.main;
        WeaponMagazineBullet?.Invoke(currentBullet, magazineBullet);
    }
    protected void Fire(bool fireInput)
    {
        if (Time.time > weaponCoolDownTimer && fireInput)
        {
            if (CanFire && IsFire)
            {
                //center
                Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    if (currentBullet > 0)
                        currentBullet--;
                    WeaponMagazineBullet?.Invoke(currentBullet, magazineBullet);
                    muzzleEffect.Play();
                }
            }
            weaponCoolDownTimer = Time.time + weaponCoolDown;  
        }
    }
    protected void FireAnim(bool fire)
    {
        if (CanFire && IsFire && fire && currentBullet > 0)
        {
            weaponAnims.SetFloat("Fire", 1f);        
        }
        else
            weaponAnims.SetFloat("Fire", -1f);
    }
    protected void Reload()
    {
        if (magazineBullet > 0 && IsFire)
        {
            if (currentBullet == 0)
            {
                defaultReloadTime = fullReloadTime;
                weaponAnims.SetTrigger("Full Reload");
            }
            else
            {
                defaultReloadTime = reloadTime;
                weaponAnims.SetTrigger("Reload");
            }
            StartCoroutine(ReloadTime());
        }
        else
            Debug.Log("You need bullet");
    }
    protected IEnumerator ReloadTime()
    {
        IsFire = false;
        yield return new WaitForSeconds(defaultReloadTime);

        var neededBullet = maxBullet - currentBullet;
        if(magazineBullet>=neededBullet)
        {
            currentBullet = currentBullet + neededBullet;
            magazineBullet = magazineBullet - neededBullet;
        }
        else
        {
            currentBullet = currentBullet + magazineBullet;
            magazineBullet = 0f;
        }
        WeaponMagazineBullet?.Invoke(currentBullet,magazineBullet);
        IsFire = true;
    }
}