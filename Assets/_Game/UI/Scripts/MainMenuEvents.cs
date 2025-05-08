using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    //visual element submenus
    private VisualElement _rootMenu;

    private Button _button;
    private Button _resetScoreButton;
    private Button _quitButton;

    [SerializeField] private string _startLevelName;

    private List<Button> _menuButtons = new List<Button>();
    private AudioSource _audioSource;
    private Label _currentScoreValue;
    private Label _highScoreValue;
    private int currentScore;
    private int highScore;

    private void Awake()
    {
        SaveManager.Instance.Load();
        _audioSource = gameObject.GetComponent<AudioSource>();
        currentScore = SaveManager.Instance.ActiveSaveData.CurrentScore;
        highScore = SaveManager.Instance.ActiveSaveData.HighScore;

        _document = GetComponent<UIDocument>();

        _rootMenu = _document.rootVisualElement.Q("RootMenu");

        _currentScoreValue = _document.rootVisualElement.Q("CurrentValue") as Label;
        _highScoreValue = _document.rootVisualElement.Q("BestValue") as Label;

        _currentScoreValue.text = currentScore.ToString();
        _highScoreValue.text = highScore.ToString();

        _button = _document.rootVisualElement.Q("PlayMainMenuButton") as Button;
        _resetScoreButton = _document.rootVisualElement.Q("ResetMainMenuButton") as Button;
        _quitButton = _document.rootVisualElement.Q("QuitMainMenuButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameCLick);
        _resetScoreButton.RegisterCallback<ClickEvent>(OnResetButtonClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {

        _button.UnregisterCallback<ClickEvent>(OnPlayGameCLick);
        _resetScoreButton.UnregisterCallback<ClickEvent>(OnResetButtonClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }

    private void OnPlayGameCLick(ClickEvent evt)
    {
        //Debug.Log("You pressed the start button");
        SaveManager.Instance.ActiveSaveData.CurrentScore = currentScore;
        SaveManager.Instance.ActiveSaveData.HighScore = highScore;
        SaveManager.Instance.Save();
        SceneManager.LoadScene(_startLevelName);
    }

    private void OnResetButtonClick(ClickEvent evt)
    {
        SaveManager.Instance.ResetSave();
        SaveManager.Instance.Save();
        currentScore = SaveManager.Instance.ActiveSaveData.CurrentScore;
        highScore = SaveManager.Instance.ActiveSaveData.HighScore;
        _currentScoreValue.text = currentScore.ToString();
        _highScoreValue.text = highScore.ToString();
    }

    private void OnQuitButtonClick(ClickEvent evt)
    {
        Application.Quit();
    }
}
