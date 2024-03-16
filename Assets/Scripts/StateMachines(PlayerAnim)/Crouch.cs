using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : BaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Crouching",true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if(Input.GetKey(KeyCode.LeftShift))
        ExitState(movement,movement.Run);
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(movement.moveDirection.magnitude<0.1f)
            ExitState(movement,movement.Idle);
            else
            ExitState(movement,movement.Walk);
        }
         if(movement.vInput<0)
        movement.CurrentMoveSpeed=movement.CrouchBackSpeed;
        else
        movement.CurrentMoveSpeed=movement.CrouchSpeed;
    }
    void ExitState(MovementStateManager movement,BaseState state)
    {
        movement.anim.SetBool("Crouching",false);
        movement.SwitchState(state);
    }
}
