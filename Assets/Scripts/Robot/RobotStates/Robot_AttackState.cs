using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_AttackState : Robot_State
{
    private Animator animator;

    private AudioSource audioSource;

    public Robot_AttackState(Robot_Controller robotController, Robot_StateMachine stateMachine, Character_Data robotData, Animator animator, AudioSource audioSource) : base(robotController, stateMachine, robotData)
    {
        this.animator = animator;
        this.audioSource = audioSource;
    }

    public override void Enter()
    {
        base.Enter();

        robotController.ReproduceSound(audioSource, robotData.attackNoImpactSound, false);

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
