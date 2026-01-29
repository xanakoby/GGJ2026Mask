using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GenericStateMachine<ECharacterState> StateMachine;
    public char InputPressed;
    private void Awake()
    {
        StateMachine = new GenericStateMachine<ECharacterState>();

        StateMachine.RegisterState(ECharacterState.Idle, new IdleCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Walking, new WalkingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Jumping, new JumpingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Falling, new FallingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Landing, new LandingCharacterState(this));

        SetState(ECharacterState.Idle);
    }

    public void SetState(ECharacterState newState)
    {
        StateMachine.SetState(newState);
    }

    void Update()
    {
        HandleInput();

        StateMachine.OnUpdate();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            InputPressed = 'W';
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            InputPressed = 'A';
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            InputPressed = 'S';
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            InputPressed = 'D';
        }
    }
}
