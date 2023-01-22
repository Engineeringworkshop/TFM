using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_DefenseState : Robot_State
{
    private Animator animator;

    public Robot_DefenseState(Robot_Controller robotController, Robot_StateMachine stateMachine, Character_Data robotData, Animator animator) : base(robotController, stateMachine, robotData)
    {
        this.animator = animator;
    }

    public override void Enter()
    {
        base.Enter();

        animator.SetBool("defend", true);

        robotController.IsDefending = true;
    }

    public override void Exit()
    {
        base.Exit();

        animator.SetBool("defend", false);

        robotController.IsDefending = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Input.GetKeyUp(robotData.defenseKey))
        {
            robotController.robotAnimatorController.UnFreezeAnimation();

            robotController.StartCoroutine(robotController.WaitAnimationToFinish(robotController.RobotIdleState));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
