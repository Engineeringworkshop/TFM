using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State
{
    protected Robot_Controller robotController;
    protected Robot_StateMachine stateMachine;

    protected float startTime; // Referencia para saber cuanto lleva en cada estado

    private string animBoolName; // En esta variable se guardará informacion para las animaciones, así el animator sabrá que animación deberá usar.

    public Robot_State(Robot_Controller robotController, Robot_StateMachine stateMachine)
    {
        this.robotController = robotController;
        this.stateMachine = stateMachine;
    }

    // Enter() se ejecutará al entrar en un estado
    public virtual void Enter()
    {
        Debug.Log("Zombie1: " + animBoolName);
    }

    // Exit() se ejecutará al salir del estado
    public virtual void Exit()
    {
        // Comprobamos si el cambio tiene animación para evitar advertencias 
        if (animBoolName != "")
        {
            //zombie1.Anim.SetBool(animBoolName, false); // ponemos el animator en false al salir
        }

    }

    // LogicUpdate() se ejecutará en cada Update()
    public virtual void LogicUpdate()
    {

    }

    // PhysicsUpdate se ejecutará en cada FixedUpdate()
    public virtual void PhysicsUpdate()
    {

    }
}
