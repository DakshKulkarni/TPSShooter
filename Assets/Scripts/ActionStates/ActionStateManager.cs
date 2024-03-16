using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class ActionStateManager : MonoBehaviour
{
  public  ActionBaseState currentState;
  public  DefaultState Default=new DefaultState();
   public ReloadState Reload=new ReloadState();
    public GameObject currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;
    AudioSource audioSource;
    [HideInInspector] public Animator anim;
    public MultiAimConstraint rightHandAim;
    public TwoBoneIKConstraint leftHandIK;
    void Start()
    {
        SwitchState(Default);
        anim=GetComponent<Animator>();
        ammo=currentWeapon.GetComponent<WeaponAmmo>();
        audioSource=GetComponent<AudioSource>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    public void WeaponReloaded()
    {
       ammo.Reload();
       rightHandAim.weight=1;
       leftHandIK.weight=1;
       SwitchState(Default);
    }
    public void MagOut()
    {
        audioSource.PlayOneShot(ammo.magOutSound);
    }
     public void MagIn()
    {
         audioSource.PlayOneShot(ammo.magInSound);
    }
     public void ReleaseSlide()
    {
         audioSource.PlayOneShot(ammo.releaseSlideSound);
    }
}
