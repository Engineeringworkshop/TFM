using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (!GameplayManager.IsPaused)
        {
            // Check movements controls
            CheckMovementControls();

            // Check action buttons
            CheckActionControls();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckMovementControls()
    {
        if (Input.GetKeyDown(robotData.moveLeftKey) || Input.GetKey(robotData.moveLeftKey))
        {
            stateMachine.ChangeState(robotController.RobotWalkState);

            robotController.MoveInput = 1;
        }

        if (Input.GetKeyDown(robotData.moveRightKey) || Input.GetKey(robotData.moveRightKey))
        {
            stateMachine.ChangeState(robotController.RobotWalkState);

            robotController.MoveInput = -1;
        }
    }

    private void CheckActionControls()
    {
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
}
