using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_WalkState : Robot_State
{
    private Animator animator;

    private AudioSource audioSource;

    public Robot_WalkState(Robot_Controller robotController, Robot_StateMachine stateMachine, Character_Data robotData, Animator animator, AudioSource audioSource) : base(robotController, stateMachine, robotData)
    {
        this.animator = animator;
        this.audioSource = audioSource;
    }

    public override void Enter()
    {
        base.Enter();

        robotController.ReproduceSound(audioSource, robotData.walkSound, true);
    }

    public override void Exit()
    {
        base.Exit();

        // Stop walking sound
        audioSource.Stop();

        // Stop robot movement
        robotController.MoveInput = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Input.GetKeyUp(robotData.moveLeftKey) || Input.GetKeyUp(robotData.moveRightKey))
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
