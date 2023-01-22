using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State
{
    protected Robot_Controller robotController;
    protected Robot_StateMachine stateMachine;
    protected Character_Data robotData;

    public Robot_State(Robot_Controller robotController, Robot_StateMachine stateMachine, Character_Data robotData)
    {
        this.robotController = robotController;
        this.stateMachine = stateMachine;
        this.robotData = robotData;
    }

    // Enter() will executed when enter the state
    public virtual void Enter()
    {
        Debug.Log("Enter state" + this);
    }

    // Exit() will executed when exit the state
    public virtual void Exit()
    {

    }

    // LogicUpdate() will executed on Update()
    public virtual void LogicUpdate()
    {

    }

    // PhysicsUpdate will executed on FixedUpdate()
    public virtual void PhysicsUpdate()
    {

    }
}
