using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public BaseState previousState;
    public BaseState currentState;
    public Idle Idle=new Idle();
    public Walk Walk=new Walk();
    public Crouch Crouch=new Crouch();
    public Run Run=new Run();
    public JumpState Jump = new JumpState();
    public float CurrentMoveSpeed;
    public float WalkSpeed=3f, WalkBackSpeed=2f;
    public float RunSpeed=6f, RunBackSpeed=4f;
    public float CrouchSpeed=2f, CrouchBackSpeed=1f;
    public CharacterController controller;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float jumpForce=10;
    [HideInInspector] public bool jumped;
    Vector3 velocity;
    [HideInInspector] public Animator anim;
    public float vInput;
    public float hInput;
    public Vector3 moveDirection;

    void Start()
    {
        anim=GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        SwitchState(Idle);
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        Falling();
        anim.SetFloat("hzInput",hInput);
        anim.SetFloat("vInput",vInput);
        currentState.UpdateState(this);
    }
    public void SwitchState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        moveDirection = (cameraForward * vInput + cameraRight * hInput).normalized;
        controller.Move(moveDirection * CurrentMoveSpeed * Time.deltaTime);
    }

  public bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        float maxDistance = controller.height / 2 + groundYOffset + 0.1f;
        if (Physics.Raycast(ray, maxDistance, groundLayer))
            return true;
        else
            return false;
    }
    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    void Falling()
    {
        anim.SetBool("Falling", !IsGrounded());
    }
    public void JumpForce()
    {
        velocity.y+=jumpForce;
    }
    public void Jumped()
    {
        jumped=true;
    }
}
