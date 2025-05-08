using UnityEngine;

public class GamePauseState : State
{
    public GameFSM _stateMachine;
    public GameController _controller;

    public GamePauseState(GameFSM StateMachine, GameController controller)
    {
        _stateMachine = StateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Paused State");
        Time.timeScale = 0f;
        SaveManager.Instance.Load();
        Debug.Log("Listen for Player Inputs");
        _controller.HUDController.PlayGame += OnGamePlay;
        Debug.Log("Display Player HUD");
        _controller.HUDController.DisplayScores();
    }

    public override void Exit()
    {
        _controller.HUDController.SaveScores();
        _controller.HUDController.PlayGame -= OnGamePlay;
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
    }

    private void OnGamePlay()
    {
        _stateMachine.ChangeStateToPrevious();
    }
}
