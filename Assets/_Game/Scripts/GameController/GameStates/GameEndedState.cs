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
        Time.timeScale = 0f;
        _clickedThrough = false;
        Debug.Log("Listen for Player Inputs");
        _controller.HUDController.SetupGame += OnGameReset;
        Debug.Log("Display Player HUD");
        _controller.HUDController.OnGameOver();
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
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

    private void OnGameReset()
    {
        // Exit game by choosing any option on screen: retry, quit
        Debug.Log("Exit Lose State");
        _clickedThrough = true;
    }
}
