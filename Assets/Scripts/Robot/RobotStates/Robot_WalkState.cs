using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        //robotController.MoveInput = 0;

        animator.SetFloat("speed", 0);
        animator.SetFloat("direction", robotController.MoveInput * 1);

        // Stop robot rigidbody
        robotController.StopCharacter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Math.Abs(robotController.MoveInput) < robotController.movementDeadzone)
        {
            stateMachine.ChangeState(robotController.RobotIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!GameplayManager.IsPaused)
        {
            // Move character
            robotController.MoveCharacter();

            animator.SetFloat("speed", Mathf.Abs(robotController.MoveInput));
            animator.SetFloat("direction", robotController.MoveInput * 1);
        }
    }
}
