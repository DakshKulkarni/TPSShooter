using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] float defaultBloomAngle=3;
    [SerializeField] float walkBloomMultiplier=1.5f;
    [SerializeField] float crouchBloomMultiplier=0.5f;
    [SerializeField] float sprintBloomMultiplier=2f;
    [SerializeField] float adsBloomMultiplier=0.5f;
    float currentBloom;
    MovementStateManager movement;
    AimStateManager aim;
    void Start()
    {
        movement=GetComponentInParent<MovementStateManager>();
        aim=GetComponentInParent<AimStateManager>();
    }
    public Vector3 BloomAngle(Transform barrelposition)
    {
        if(movement.currentState==movement.Idle)
        currentBloom=defaultBloomAngle;

        else if(movement.currentState==movement.Walk)
        currentBloom=defaultBloomAngle*walkBloomMultiplier;

        else if(movement.currentState==movement.Run)
        currentBloom=defaultBloomAngle*sprintBloomMultiplier;

        else if(movement.currentState==movement.Crouch)
        {
            if(movement.moveDirection.magnitude==0)
            currentBloom=defaultBloomAngle * crouchBloomMultiplier;
            else
            {
                currentBloom=defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier;
            }
        }

        if(aim.currentState==aim.Aim)
        currentBloom*=adsBloomMultiplier;
        float randX=Random.Range(-currentBloom,currentBloom);
        float randY=Random.Range(-currentBloom,currentBloom);
        float randZ=Random.Range(-currentBloom,currentBloom);
        Vector3 randomRotation=new Vector3(randX,randY,randZ);
       return barrelposition.localEulerAngles+randomRotation;
    }
  
}
