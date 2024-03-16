using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;
    [Header("Bullet Settings")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    AimStateManager aim;
    WeaponAmmo ammo;
    WeaponBloom bloom;
    [SerializeField] AudioClip gunShot;
    WeaponRecoil recoil;
    AudioSource audioSource;
    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed=20;
    private AmmoCount ammoCount;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        aim=GetComponentInParent<AimStateManager>();
        ammo=GetComponent<WeaponAmmo>();
        bloom=GetComponent<WeaponBloom>();
        recoil=GetComponent<WeaponRecoil>();
        muzzleFlashLight=GetComponentInChildren<Light>();
        muzzleFlashParticles=GetComponentInChildren<ParticleSystem>();
        lightIntensity=muzzleFlashLight.intensity;
        muzzleFlashLight.intensity=0;
        fireRateTimer=fireRate;
        ammoCount=GameObject.Find("Ammo").GetComponent<AmmoCount>(); 
    }
    void Update()
    {
        if(ShouldFire())
        Fire();
        muzzleFlashLight.intensity=Mathf.Lerp(muzzleFlashLight.intensity,0,lightReturnSpeed*Time.deltaTime);
    }
    bool ShouldFire()
    {
        fireRateTimer+=Time.deltaTime;
        if(fireRateTimer<fireRate)
        return false;
        if(ammo.currentAmmo==0)
        return false;
        if(semiAuto && Input.GetKeyDown(KeyCode.Mouse0))
        return true;
        if(!semiAuto && Input.GetKey(KeyCode.Mouse0))
        return true;
        return false;
    }
    void Fire()
    {
        fireRateTimer=0;
        barrelPos.LookAt(aim.aimPosition);
        barrelPos.localEulerAngles=bloom.BloomAngle(barrelPos);
        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        ammo.currentAmmo--;
        UpdateAmmoUI(); 
        for(int i=0; i<bulletPerShot; i++)
        {
            GameObject newBullet=Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            newBullet.GetComponent<Rigidbody>().velocity=barrelPos.forward*bulletVelocity;
        }
    }
    void TriggerMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity=lightIntensity;
    }
     private void UpdateAmmoUI()
    {
        if (ammoCount != null)
        {
            ammoCount.UpdateAmmo(ammo.currentAmmo, ammo.extraAmmo);
        }
    }
}
