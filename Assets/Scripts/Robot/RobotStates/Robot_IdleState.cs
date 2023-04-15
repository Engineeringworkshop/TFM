using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Robot_IdleState : Robot_State
{
    public Robot_IdleState(Robot_Controller robotController, Robot_StateMachine stateMachine, Character_Data robotData) : base(robotController, stateMachine, robotData)
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

        if (Math.Abs(robotController.MoveInput) > robotController.movementDeadzone)
        {
            stateMachine.ChangeState(robotController.RobotWalkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
