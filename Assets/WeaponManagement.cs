using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    public int GunsCount;
    public int CurGunIndex = 1;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            CurGunIndex++;
            CurGunIndex %= GunsCount;
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            CurGunIndex--;
            if (CurGunIndex < 0) CurGunIndex = GunsCount - 1;         
        }
        SetWeapon();
    }

    private void SetWeapon()
    {
        var index = 0;
        foreach (Transform weapon in transform)
        {
            if (index == CurGunIndex)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            index++;
        }
    }
}
