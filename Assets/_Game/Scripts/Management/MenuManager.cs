using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// Manages the menu interactions and scene transitions.
/// </summary>
public class MenuManager : MonoBehaviour
{
    //PARAMS
    #region PARAMS
    [Header("MenuManager Params")]
    [Tooltip("List of LeafButton components in the menu.")]
    [SerializeField] private List<LeafButton> _leafButtons;

    [Header("Music Params")]
    [Tooltip("List of background music clips.")]
    [SerializeField] private List<AudioClip> _musicClips;
    //TextMeshPro component for the music button
    private TextMeshProUGUI _musicButtonText;
    [Tooltip("LeafButton component for the music button.")]
    [SerializeField] private LeafButton _musicButton;
    [Tooltip("AudioSource component for the background music.")]
    [SerializeField] private AudioSource _audioSource;
    #endregion

    private int _currentButtonIndex = 0;
    private int _currentMusicIndex = 0;

    private void Awake()
    {
        // Ensure there are LeafButtons in the list
        if (_leafButtons == null || _leafButtons.Count == 0)
        {
            Debug.LogError("No LeafButtons assigned in the MenuManager.");
            return;
        }

        // Ensure there are music clips
        if (_musicClips == null || _musicClips.Count == 0)
        {
            Debug.LogError("No music clips assigned in the MenuManager.");
            return;
        }

        // Get the AudioSource component
        _audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("No AudioSource found on BackgroundMusic object.");
            return;
        }

        // Assign functions to buttons
        foreach (var button in _leafButtons)
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

        // Assign function to the music button
        if (_musicButton != null)
        {
            _musicButtonText = _musicButton.GetComponent<TextMeshProUGUI>();
            _musicButton.OnPressed.AddListener(ChangeMusic);
        }
        else
        {
            Debug.LogError("MusicButton not assigned in the MenuManager.");
        }
    }

    private void Start()
    {
        // Highlight the first button initially
        HoverButton(_currentButtonIndex);
        // Start the first music track
        PlayMusic(_currentMusicIndex);
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
        _currentButtonIndex = (_currentButtonIndex + 1) % _leafButtons.Count;
        HoverButton(_currentButtonIndex);
    }

    /// <summary>
    /// Moves the highlight to the previous button in the list.
    /// </summary>
    private void MoveToPreviousButton()
    {
        _currentButtonIndex = (_currentButtonIndex - 1 + _leafButtons.Count) % _leafButtons.Count;
        HoverButton(_currentButtonIndex);
    }

    /// <summary>
    /// Triggers the press action of the currently highlighted button.
    /// </summary>
    private void PressCurrentButton()
    {
        var currentButton = _leafButtons[_currentButtonIndex];
        ExecuteEvents.Execute(currentButton.gameObject, new PointerEventData(EventSystem.current),
            ExecuteEvents.pointerDownHandler);
    }

    /// <summary>
    /// Highlights the button at the specified index.
    /// </summary>
    /// <param name="index">Index of the button to highlight.</param>
    private void HoverButton(int index)
    {
        for (int i = 0; i < _leafButtons.Count; i++)
        {
            if (i == index)
            {
                ExecuteEvents.Execute(_leafButtons[i].gameObject, new PointerEventData(EventSystem.current),
                    ExecuteEvents.pointerEnterHandler);

                //Debug.Log(leafButtons[_currentButtonIndex].name + " Hover");
            }
            else
            {
                ExecuteEvents.Execute(_leafButtons[i].gameObject, new PointerEventData(EventSystem.current),
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

    /// <summary>
    /// Changes to the next music track and updates the button text.
    /// </summary>
    private void ChangeMusic()
    {
        _currentMusicIndex = (_currentMusicIndex + 1) % _musicClips.Count;
        PlayMusic(_currentMusicIndex);
    }

    /// <summary>
    /// Plays the specified music track with fade-in and pitch effects.
    /// </summary>
    /// <param name="index">Index of the music track to play.</param>
    private void PlayMusic(int index)
    {
        _audioSource.DOFade(0, 0.5f).OnComplete(() =>
        {
            _audioSource.clip = _musicClips[index];
            _audioSource.Play();
            _audioSource.DOFade(1, 0.5f);
            _musicButtonText.text = _audioSource.clip.name;
        });
        _audioSource.DOPitch(1.5f, 0.5f).SetLoops(2, LoopType.Yoyo);
    }

    public void OpenUrl(string URL)
    {
        Application.OpenURL(URL);
    }
}