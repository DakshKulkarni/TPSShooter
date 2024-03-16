using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : BaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Walking",true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if(Input.GetKey(KeyCode.LeftShift))
        ExitState(movement,movement.Run);
        else if(Input.GetKeyUp(KeyCode.C))
        ExitState(movement,movement.Crouch);
        else if(movement.moveDirection.magnitude<0.1f)
        ExitState(movement,movement.Idle);
        if(movement.vInput<0)
        movement.CurrentMoveSpeed=movement.WalkBackSpeed;
        else
        movement.CurrentMoveSpeed=movement.WalkSpeed;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState=this;
            ExitState(movement,movement.Jump);
        }
        
    }
    void ExitState(MovementStateManager movement,BaseState state)
    {
        movement.anim.SetBool("Walking",false);
        movement.SwitchState(state);
    }
}
