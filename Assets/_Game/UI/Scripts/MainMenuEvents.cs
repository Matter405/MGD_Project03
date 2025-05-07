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

    [SerializeField] private string _startLevelName;

    private List<Button> _menuButtons = new List<Button>();
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();

        _document = GetComponent<UIDocument>();

        _rootMenu = _document.rootVisualElement.Q("RootMenu");

        _button = _document.rootVisualElement.Q("PlayMainMenuButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameCLick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {

        _button.UnregisterCallback<ClickEvent>(OnPlayGameCLick);
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
        SceneManager.LoadScene(_startLevelName);
    }
}
