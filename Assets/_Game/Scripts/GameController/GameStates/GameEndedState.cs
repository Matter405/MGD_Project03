using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndedState : State
{
    public GameFSM _stateMachine;
    public GameController _controller;
    public bool _clickedThrough = false;

    // this is our 'constructor' , called when this state is created
    public GameEndedState(GameFSM StateMachine, GameController controller)
    {
        // hold on to our parameters in our class variables for reuse
        _stateMachine = StateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Ended State");
        _clickedThrough = false;
        Debug.Log("Listen for Player Inputs");
        _controller.Input.TouchStarted += OnTouchPressed;
        Debug.Log("Display Player HUD");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();

        if (_clickedThrough)
            _stateMachine.ChangeState(_stateMachine.SetupState);
        // ClickedThrough State, reload level, change back to SetupState, etc.
    }

    private void OnTouchPressed(Vector2 position)
    {
        Debug.Log("Exit Lose State");
        _clickedThrough = true;
    }
}
