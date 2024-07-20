using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// Manages the menu interactions and scene transitions.
/// </summary>
public class MenuManager : MonoBehaviour
{
    //PARAMS
    #region PARAMS
    [Header("MenuManager Params")]
    [Tooltip("List of LeafButton components in the menu.")]
    [SerializeField] private List<LeafButton> leafButtons;
    #endregion

    private int _currentButtonIndex = 0;

    private void Awake()
    {
        // Ensure there are LeafButtons in the list
        if (leafButtons == null || leafButtons.Count == 0)
        {
            Debug.LogError("No LeafButtons assigned in the MenuManager.");
            return;
        }

        // Assign functions to buttons
        foreach (var button in leafButtons)
        {
            if (button.name == "StartButton")
            {
                button.OnPressed.AddListener(StartLevel);
            }
            else if (button.name == "QuitButton")
            {
                button.OnPressed.AddListener(Quit);
            }
        }
    }

    private void Start()
    {
        // Highlight the first button initially
        HoverButton(_currentButtonIndex);
    }

    private void Update()
    {
        HandleKeyboardInput();
    }

    /// <summary>
    /// Handles keyboard inputs for navigating and interacting with the menu buttons.
    /// </summary>
    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToNextButton();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToPreviousButton();
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            PressCurrentButton();
        }
    }

    /// <summary>
    /// Moves the highlight to the next button in the list.
    /// </summary>
    private void MoveToNextButton()
    {
        _currentButtonIndex = (_currentButtonIndex + 1) % leafButtons.Count;
        HoverButton(_currentButtonIndex);
    }

    /// <summary>
    /// Moves the highlight to the previous button in the list.
    /// </summary>
    private void MoveToPreviousButton()
    {
        _currentButtonIndex = (_currentButtonIndex - 1 + leafButtons.Count) % leafButtons.Count;
        HoverButton(_currentButtonIndex);
    }

    /// <summary>
    /// Triggers the press action of the currently highlighted button.
    /// </summary>
    private void PressCurrentButton()
    {
        var currentButton = leafButtons[_currentButtonIndex];
        ExecuteEvents.Execute(currentButton.gameObject, new PointerEventData(EventSystem.current),
            ExecuteEvents.pointerDownHandler);
    }

    /// <summary>
    /// Highlights the button at the specified index.
    /// </summary>
    /// <param name="index">Index of the button to highlight.</param>
    private void HoverButton(int index)
    {
        for (int i = 0; i < leafButtons.Count; i++)
        {
            if (i == index)
            {
                ExecuteEvents.Execute(leafButtons[i].gameObject, new PointerEventData(EventSystem.current),
                    ExecuteEvents.pointerEnterHandler);

                Debug.Log(leafButtons[_currentButtonIndex].name + " Hover");
            }
            else
            {
                ExecuteEvents.Execute(leafButtons[i].gameObject, new PointerEventData(EventSystem.current),
                    ExecuteEvents.pointerExitHandler);
            }
        }
    }

    //START
    #region START
    private void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
    #endregion

    //QUIT
    #region QUIT
    private void Quit()
    {
        Application.Quit();
    }
    #endregion

    public void OpenUrl(string URL)
    {
        Application.OpenURL(URL);
    }
}