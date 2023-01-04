using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_IdleState : Robot_State
{
    public Robot_IdleState(Robot_Controller robotController, Robot_StateMachine stateMachine) : base(robotController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
