using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class AimStateManager : MonoBehaviour
{
   public AimBaseState currentState;
   public HipFire Hip=new HipFire();
   public AimState Aim = new AimState();
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPosition;
    [SerializeField] float sensitivity = 2f;
    [HideInInspector]public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFOV=30f;
    [HideInInspector] public float hipFOV;
    [HideInInspector] public float currentFOV;
    public float fovSmoothSpeed=15;
    public Transform aimPosition;
    [HideInInspector] public Vector3 actualAimPosition;
    [SerializeField] float aimSmoothSpeed=20;
    [SerializeField] LayerMask aimMask;
 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        vCam=GetComponentInChildren<CinemachineVirtualCamera>();
        hipFOV=vCam.m_Lens.FieldOfView;
        anim=GetComponentInChildren<Animator>();
        SwitchState(Hip);
    }
    void Update()
    {
        xAxis+=Input.GetAxisRaw("Mouse X") * sensitivity;
        yAxis-=Input.GetAxisRaw("Mouse Y") * sensitivity;
        yAxis=Mathf.Clamp(yAxis,-80,80);
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFOV, Time.deltaTime * fovSmoothSpeed);
        Vector2 screenCentre= new Vector2(Screen.width/2f,Screen.height/2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);
        if(Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,aimMask))
        {
            aimPosition.position=Vector3.Lerp(aimPosition.position,hit.point,Time.deltaTime * aimSmoothSpeed);
            actualAimPosition=hit.point;
        }
        currentState.UpdateState(this);
    }
    private void LateUpdate()
    {
        camFollowPosition.localEulerAngles = new Vector3(yAxis, camFollowPosition.localEulerAngles.y, camFollowPosition.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }
    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    
}
