using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
   [SerializeField] Transform recoilFollowPos;
   [SerializeField] float kickBackAmount=-1;
   [SerializeField] float kickBackSpeed=5, returnSpeed=10;
    float currentRecoilPosition, finalRecoilPosition;
    void Update()
    {
        currentRecoilPosition=Mathf.Lerp(currentRecoilPosition,0,returnSpeed*Time.deltaTime);
        finalRecoilPosition=Mathf.Lerp(finalRecoilPosition,0,kickBackSpeed*Time.deltaTime);
        recoilFollowPos.localPosition=new Vector3(0,0,finalRecoilPosition+currentRecoilPosition);
    }
    public void TriggerRecoil()
    {
        currentRecoilPosition+=kickBackAmount;
    }
}
