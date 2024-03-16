using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;
    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;
    private AmmoCount ammoCount;

    void Start()
    {
        currentAmmo = clipSize;
        ammoCount = GameObject.Find("Ammo").GetComponent<AmmoCount>();
        UpdateAmmoCountUI();
    }
    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {
                int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftOverAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
        UpdateAmmoCountUI();
    }
    private void UpdateAmmoCountUI()
    {
        if (ammoCount != null)
        {
            ammoCount.UpdateAmmo(currentAmmo, extraAmmo);
        }
    }
}
