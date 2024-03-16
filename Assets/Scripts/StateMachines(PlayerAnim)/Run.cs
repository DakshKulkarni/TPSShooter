using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : BaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Running",true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if(Input.GetKeyUp(KeyCode.LeftShift))
        ExitState(movement,movement.Walk);
        else if(movement.moveDirection.magnitude<0.1f)
        ExitState(movement,movement.Idle);
         if(movement.vInput<0)
        movement.CurrentMoveSpeed=movement.RunBackSpeed;
        else
        movement.CurrentMoveSpeed=movement.RunSpeed;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState=this;
            ExitState(movement,movement.Jump);
        }
    }
        void ExitState(MovementStateManager movement,BaseState state)
    {
        movement.anim.SetBool("Running",false);
        movement.SwitchState(state);
    }
}
