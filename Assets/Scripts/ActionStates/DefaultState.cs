using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState :ActionBaseState
{
   public override void EnterState(ActionStateManager actions)
    {
        actions.rightHandAim.weight=1;
        actions.leftHandIK.weight=1;
    }
    public override void UpdateState(ActionStateManager actions)
    {
        actions.rightHandAim.weight= Mathf.Lerp(actions.rightHandAim.weight,0.5f,10 * Time.deltaTime);
        actions.leftHandIK.weight= Mathf.Lerp(actions.leftHandIK.weight,0.5f,10 * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.SwitchState(actions.Reload);
        }
    }
    bool CanReload(ActionStateManager action)
    {
        if(action.ammo.currentAmmo==action.ammo.clipSize)
        return false;
        else if(action.ammo.extraAmmo==0)
        return false;
        else 
        return true;
    }
}
