using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_StateMachine
{

    // Variable que guardará la referencia al estado actual
    public Robot_State CurrentState { get; private set; }

    // inicializa el estado inicial.
    public void Initialize(Robot_State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // Metodo que cambiará de estado, llama al exit del estado anterior, cambia el estado actual, y llama al Enter del nuevo estado actual.
    public void ChangeState(Robot_State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
