using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the level and UI elements using DOTween for smooth animations.
/// </summary>
public class LevelManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField, Tooltip("Pause menu UI element")]
    private CanvasGroup _pauseMenu;

    [SerializeField, Tooltip("Resume button UI element")]
    private EventTrigger _resumeButton;

    [SerializeField, Tooltip("Restart button UI element")]
    private EventTrigger _restartButton;

    [SerializeField, Tooltip("Menu button UI element")]
    private EventTrigger _menuButton;

    [SerializeField, Tooltip("Pause button UI element")]
    private EventTrigger _nextButton;

    [SerializeField, Tooltip("Pause button UI element")]
    private EventTrigger _pauseButton;

    [SerializeField, Tooltip("Level complete text UI element")]
    private TextMeshProUGUI _levelCompleteText;

    [SerializeField, Tooltip("Level fail text UI element")]
    private TextMeshProUGUI _levelFailText;

    [SerializeField, Tooltip("Game pause text UI element")]
    private TextMeshProUGUI _gamePausedText;

    [SerializeField, Tooltip("Countdown text UI element")]
    private TextMeshProUGUI _countdownText;

    private int _currentLevelIndex = 0;

    [Header("Tutroial Params")]
    [SerializeField]
    private MenuView _tutorial;

    private void Start()
    {
        // Initialize buttons with corresponding functions
        AddEventTrigger(_resumeButton, EventTriggerType.PointerDown, Resume);
        AddEventTrigger(_restartButton, EventTriggerType.PointerDown, Restart);
        AddEventTrigger(_menuButton, EventTriggerType.PointerDown, Menu);
        AddEventTrigger(_nextButton, EventTriggerType.PointerDown, Next);
        AddEventTrigger(_pauseButton, EventTriggerType.PointerDown, Pause);

        // Hide pause menu and end level texts at the start
        _pauseMenu.alpha = 0;
        _pauseMenu.gameObject.SetActive(false);
        _levelCompleteText.gameObject.SetActive(false);
        _levelFailText.gameObject.SetActive(false);
        _gamePausedText.gameObject.SetActive(false);
        _countdownText.gameObject.SetActive(false);

        if (_tutorial)
            _tutorial.Open();

        StartCountDown();
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => { action.Invoke(); });
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// Starts the level and initializes any required elements.
    /// </summary>
    public void LevelStart()
    {
        // Initialize level start logic here
        Debug.Log("Level Started");
    }

    /// <summary>
    /// Starts the countdown before the level begins.
    /// </summary>
    public void StartCountDown()
    {
        _countdownText.gameObject.SetActive(true);

        Sequence countdownSequence = DOTween.Sequence();
        countdownSequence.Append(_countdownText.DOFade(0, 0f))
                         .Append(_countdownText.DOFade(1, 0.5f).OnStart(() => _countdownText.text = "3"))
                         .AppendInterval(1)
                         .Append(_countdownText.DOFade(0, 0.5f))
                         .Append(_countdownText.DOFade(1, 0.5f).OnStart(() => _countdownText.text = "2"))
                         .AppendInterval(1)
                         .Append(_countdownText.DOFade(0, 0.5f))
                         .Append(_countdownText.DOFade(1, 0.5f).OnStart(() => _countdownText.text = "1"))
                         .AppendInterval(1)
                         .Append(_countdownText.DOFade(0, 0.5f))
                         .Append(_countdownText.DOFade(1, 0.5f).OnStart(() => _countdownText.text = "Start"))
                         .AppendInterval(1)
                         .Append(_countdownText.DOFade(0, 0.5f))
                         .OnComplete(() => _countdownText.gameObject.SetActive(false));

        countdownSequence.Play();
    }

    /// <summary>
    /// Marks the level as complete and shows the completion UI.
    /// </summary>
    public void LevelComplete()
    {
        _levelCompleteText.gameObject.SetActive(true);
        _levelFailText.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
        _gamePausedText.gameObject.SetActive(false);

        _resumeButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(true);
        _menuButton.gameObject.SetActive(true);
        _nextButton.gameObject.SetActive(true);

        _pauseMenu.DOFade(1, 1f).OnComplete(() =>
        {
            _levelCompleteText.DOFade(1, 1f).SetDelay(2f).OnComplete(() =>
            {
                IncreaseLevelIndex();
            });
        });
        Debug.Log("Level Complete");
    }

    /// <summary>
    /// Marks the level as fail and shows the completion UI.
    /// </summary>
    public void LevelFail()
    {
        _levelFailText.gameObject.SetActive(true);
        _levelCompleteText.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
        _gamePausedText.gameObject.SetActive(false);

        _resumeButton.gameObject.SetActive(false);
        _nextButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(true);
        _menuButton.gameObject.SetActive(true);

        _pauseMenu.DOFade(1, 1f).OnComplete(() =>
        {
            _levelFailText.DOFade(1, 1f).SetDelay(2f);
        });
        Debug.Log("Level Fail");
    }


    /// <summary>
    /// Increases the current level index and proceeds to the next level.
    /// </summary>
    public void IncreaseLevelIndex()
    {
        _currentLevelIndex++;
        Debug.Log($"Increased Level Index to {_currentLevelIndex}");
    }


    /// <summary>
    /// Pauses the game and shows the pause menu with a fade animation.
    /// </summary>
    public void Pause()
    {
        _pauseMenu.gameObject.SetActive(true);
        _gamePausedText.gameObject.SetActive(true);

        _pauseButton.gameObject.SetActive(false);
        _nextButton.gameObject.SetActive(false);

        Sequence pauseSequence = DOTween.Sequence();
        pauseSequence.Append(_pauseMenu.DOFade(1, 0.5f))
                      .AppendCallback(() =>
                      {
                          Time.timeScale = 0;
                      })
                      .OnComplete(() => Debug.Log("Game Paused"));
    }

    /// <summary>
    /// Resumes the game from pause state with a fade animation.
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        _pauseButton.gameObject.SetActive(true);
        Sequence resumeSequence = DOTween.Sequence();
        resumeSequence.Append(_pauseMenu.DOFade(0, 0.5f))
                      .OnComplete(() =>
                      {
                          _gamePausedText.gameObject.SetActive(false);
                          _pauseMenu.gameObject.SetActive(false);
                          Debug.Log("Game Resumed");
                      });
    }

    /// <summary>
    /// Restarts the current level.
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1;
        // Add logic to reload the current level here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Game Restarted");
    }

    /// <summary>
    /// Starts the next level.
    /// </summary>
    public void Next()
    {
        Time.timeScale = 1;
        // Add logic to reload the current level here
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) %
            SceneManager.sceneCountInBuildSettings);
        Debug.Log("Game Restarted");
    }

    /// <summary>
    /// Navigates to the main menu.
    /// </summary>
    public void Menu()
    {
        Time.timeScale = 1;
        // Add logic to load the main menu scene here
        SceneManager.LoadScene(0);
        Debug.Log("Navigated to Menu");
    }

    public void CloseTutorial()
    {
        if (_tutorial)
            _tutorial.Close();
    }
}