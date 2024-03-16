using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;

    private int currentAmmo;
    private int extraAmmo;

    public void UpdateAmmo(int currentAmmoValue, int extraAmmoValue)
    {
        currentAmmo = currentAmmoValue;
        extraAmmo = extraAmmoValue;

        UpdateText();
    }

    private void UpdateText()
    {
        ammoText.text = "AMMO: " + currentAmmo + "/" + extraAmmo;
    }
}
