using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI magazineBarText;

    private void Start()
    {
        WeaponManager.WeaponMagazineBullet += MagazineUpdate;
    }
    private void MagazineUpdate(float currentBullet,float magazineBullet)
    {
        magazineBarText.text = currentBullet.ToString() + "/" + magazineBullet.ToString();
    }
}
