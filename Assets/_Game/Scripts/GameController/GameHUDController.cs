using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameHUDController : MonoBehaviour
{
    [SerializeField] private AudioClip _gameMenuMusic;
    private AudioSource _audioSource;
    public int _currentScore { get; private set; }
    public int _highScore { get; private set; }

    public event Action SetupGame;
    public event Action PauseGame;
    public event Action PlayGame;
    public event Action EndGame;

    private VisualElement _gameplayMenuVisualTree;
    private VisualElement _pauseMenuVisualTree;
    private VisualElement _gameOverMenuVisualTree;

    private Label _currentScoreValueLabel;
    private Label _highScoreValueLabel;

    private Button _pauseButton;
    private Button _resumeButton;
    private Button _quitButton;
    private Button _retryButton;
    private Button _goQuitButton;
    private List<Button> _buttons = new List<Button>();

    private void Awake()
    {
        MusicManager.Instance.Play(_gameMenuMusic, 3);
        _audioSource = GetComponent<AudioSource>();
        SaveManager.Instance.Load();
        _currentScore = SaveManager.Instance.ActiveSaveData.CurrentScore;
        _highScore = SaveManager.Instance.ActiveSaveData.HighScore;

        //get the document and immediately get the root VE
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        //get each menu, shorter syntax
        _gameplayMenuVisualTree = root.Q("GameplayMenuVisualTree");
        _pauseMenuVisualTree = root.Q("PauseMenuVisualTree");
        _gameOverMenuVisualTree = root.Q("GameOverMenuVisualTree");
        //get each label and display
        _currentScoreValueLabel = root.Q("CurrentValue") as Label;
        _highScoreValueLabel = root.Q("BestValue") as Label;
        DisplayScores();
        //get pause button and register
        _pauseButton = root.Q("PauseButton") as Button;
        _pauseButton.RegisterCallback<ClickEvent>(OnPauseButtonClick);
        //get resume button and register
        _resumeButton = root.Q("ResumeButton") as Button;
        _resumeButton.RegisterCallback<ClickEvent>(OnResumeButtonClick);
        //get quit button and register
        _quitButton = root.Q("QuitButton") as Button;
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        //get retry button and register
        _retryButton = root.Q("RetryButton") as Button;
        _retryButton.RegisterCallback<ClickEvent>(OnRetryButtonClick);
        //get game over quit button and register
        _goQuitButton = root.Q("GOQuitButton") as Button;
        _goQuitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);

        //event for any button pressed
        _buttons = root.Query<Button>().ToList();
        foreach (Button button in _buttons)
        {
            button.RegisterCallback<ClickEvent>(OnAnyButtonClick);
        }
        //set initial panel visibility
        _gameplayMenuVisualTree.style.display = DisplayStyle.Flex;
        _pauseMenuVisualTree.style.display = DisplayStyle.None;
        _gameOverMenuVisualTree.style.display = DisplayStyle.None;
        SetupGame?.Invoke();
    }

    private void OnResumeButtonClick(ClickEvent evt)
    {
        Debug.Log("Resume Game!");
        //disable pause menu, enable game menu
        _pauseMenuVisualTree.style.display = DisplayStyle.None;
        _gameOverMenuVisualTree.style.display = DisplayStyle.None;
        _gameplayMenuVisualTree.style.display = DisplayStyle.Flex;
        PlayGame?.Invoke();
    }

    private void OnPauseButtonClick(ClickEvent evt)
    {
        Debug.Log("Activate Pause Menu!");
        //disable game menu, enable pause menu
        _gameplayMenuVisualTree.style.display = DisplayStyle.None;
        _gameOverMenuVisualTree.style.display = DisplayStyle.None;
        _pauseMenuVisualTree.style.display = DisplayStyle.Flex;
        PauseGame?.Invoke();
    }

    private void OnQuitButtonClick(ClickEvent evt)
    {
        //load main menu - check scene name!
        SetupGame?.Invoke();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Quit!");
    }

    private void OnRetryButtonClick(ClickEvent evt)
    {
        //load level01 - check scene name!
        SetupGame?.Invoke();
        SceneManager.LoadScene("Level01");
        Debug.Log("Retry!");
    }

    private void OnAnyButtonClick(ClickEvent evt)
    {
        //placeholder for sound effects, etc.
        Debug.Log("Any Button!");
        _audioSource.Play();
    }

    public void OnGameOver()
    {
        _pauseMenuVisualTree.style.display = DisplayStyle.None;
        _gameplayMenuVisualTree.style.display = DisplayStyle.None;
        _gameOverMenuVisualTree.style.display = DisplayStyle.Flex;
        EndGame?.Invoke();
    }

    public void DisplayScores()
    {
        _currentScoreValueLabel.text = _currentScore.ToString();
        _highScoreValueLabel.text = _highScore.ToString();
    }

    public void IncreaseScores(int scoreIncrease)
    {
        _currentScore += scoreIncrease;
        if (_currentScore > _highScore)
            _highScore = _currentScore;
    }

    public void ResetCurrentScore()
    {
        _currentScore = 0;
    }

    public void SaveScores()
    {
        SaveManager.Instance.ActiveSaveData.CurrentScore = _currentScore;
        SaveManager.Instance.ActiveSaveData.HighScore = _highScore;
        SaveManager.Instance.Save();
    }

    private void OnDisable()
    {
        _pauseButton.UnregisterCallback<ClickEvent>(OnPauseButtonClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        _resumeButton.UnregisterCallback<ClickEvent>(OnResumeButtonClick);
        _retryButton.UnregisterCallback<ClickEvent>(OnRetryButtonClick);
        _goQuitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        foreach (Button button in _buttons)
        {
            button.UnregisterCallback<ClickEvent>(OnAnyButtonClick);
        }
    }
}
