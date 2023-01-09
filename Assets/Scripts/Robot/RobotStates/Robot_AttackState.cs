using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_AttackState : Robot_State
{
    private Animator animator;

    public Robot_AttackState(Robot_Controller robotController, Robot_StateMachine stateMachine, Robot_Data robotData, Animator animator) : base(robotController, stateMachine, robotData)
    {
        this.animator = animator;
    }

    public override void Enter()
    {
        base.Enter();

        animator.SetBool("attack", true);

        robotController.StartCoroutine(robotController.WaitAnimationToFinish(robotController.RobotIdleState));
    }

    public override void Exit()
    {
        base.Exit();

        animator.SetBool("attack", false);
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
