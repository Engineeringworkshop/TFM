using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_WalkState : Robot_State
{
    private Animator animator;

    public Robot_WalkState(Robot_Controller robotController, Robot_StateMachine stateMachine, Robot_Data robotData, Animator animator) : base(robotController, stateMachine, robotData)
    {
        this.animator = animator;
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

        if (Input.GetKeyUp(robotData.moveLeftKey) || Input.GetKeyUp(robotData.moveRightKey))
        {
            stateMachine.ChangeState(robotController.RobotIdleState);

            robotController.MoveInput = 0;
        }

        if (Input.GetKeyUp(robotData.moveRightKey))
        {
            stateMachine.ChangeState(robotController.RobotIdleState);
        }

        // Attack control
        if (Input.GetKeyDown(robotData.attackKey))
        {
            stateMachine.ChangeState(robotController.RobotAttackState);
        }

        // Defense control
        if (Input.GetKeyDown(robotData.defenseKey))
        {
            stateMachine.ChangeState(robotController.RobotDefenseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
