using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    public abstract void EnterState(MovementStateManager movement);
    public abstract void UpdateState(MovementStateManager movement);
} 
