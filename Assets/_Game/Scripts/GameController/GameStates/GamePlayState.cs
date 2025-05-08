using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private bool _isPaused;
    private bool _isDead;

    // this is our 'constructor' , called when this state is created
    public GamePlayState (GameFSM StateMachine, GameController controller)
    {
        // hold on to our parameters in our class variables for reuse
        _stateMachine = StateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Play State");
        _isPaused = false;
        _isDead = false;
        Debug.Log("Listen for Player Inputs");
        _controller.HUDController.PauseGame += OnGamePaused;
        _controller.PlayerUnitPrefab.PlayerDied += OnPlayerDeath;
        Debug.Log("Display Player HUD");
    }

    public override void Exit()
    {
        //Debug.Log("Unlisten for Player Inputs");
        //Debug.Log("Hide Player HUD");
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();

        // check for lose condition
        if(_isDead)
        {
            Debug.Log("You Lose!");
            _stateMachine.ChangeState(_stateMachine.EndedState);
            // Lose State, reload level, change back to SetupState, etc.
        }
        else if (_isPaused)
        {
            _stateMachine.ChangeState(_stateMachine.PauseState);
            // Enable Pause Menu, Disable Play Menu
        }
    }

    private void OnPlayerDeath()
    {
        Debug.Log("You Died!");
        _isDead = true;
    }

    private void OnGamePaused()
    {
        Debug.Log("Game Paused");
        _isPaused = true;
    }
}
