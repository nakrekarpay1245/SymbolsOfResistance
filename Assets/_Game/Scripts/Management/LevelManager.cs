using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the level and UI elements using DOTween for smooth animations.
/// </summary>
public class LevelManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField, Tooltip("Pause menu UI element")]
    private CanvasGroup _menu;

    [SerializeField, Tooltip("Level complete background UI element")]
    private Image _levelCompleteBackground;

    [SerializeField, Tooltip("Level fail background UI element")]
    private Image _levelFailBackground;

    [SerializeField, Tooltip("Game pause background UI element")]
    private Image _gamePausedBackground;

    [SerializeField, Tooltip("Resume button UI element")]
    private EventTrigger _keepGoingButton;

    [SerializeField, Tooltip("Restart button UI element")]
    private EventTrigger _tryAgainButton;

    [SerializeField, Tooltip("Restart button UI element")]
    private EventTrigger _restartButton;

    [SerializeField, Tooltip("Menu button UI element")]
    private EventTrigger _mainMenuButton;

    [SerializeField, Tooltip("Pause button UI element")]
    private EventTrigger _continueButton;

    [SerializeField, Tooltip("Pause button UI element")]
    private EventTrigger _pauseButton;

    [SerializeField, Tooltip("Countdown text UI element")]
    private TextMeshProUGUI _countdownText;

    private int _currentLevelIndex = 0;

    [Header("Tutroial Params")]
    [SerializeField]
    private MenuView _tutorial;

    private void Start()
    {
        // Initialize buttons with corresponding functions
        AddEventTrigger(_keepGoingButton, EventTriggerType.PointerDown, Resume);
        AddEventTrigger(_tryAgainButton, EventTriggerType.PointerDown, Restart);
        AddEventTrigger(_restartButton, EventTriggerType.PointerDown, Restart);
        AddEventTrigger(_mainMenuButton, EventTriggerType.PointerDown, Menu);
        AddEventTrigger(_continueButton, EventTriggerType.PointerDown, Next);
        AddEventTrigger(_pauseButton, EventTriggerType.PointerDown, Pause);

        // Hide pause menu and end level texts at the start
        _menu.alpha = 0;
        _menu.gameObject.SetActive(false);
        _levelCompleteBackground.gameObject.SetActive(false);
        _levelFailBackground.gameObject.SetActive(false);
        _gamePausedBackground.gameObject.SetActive(false);
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
        _levelCompleteBackground.gameObject.SetActive(true);
        _levelFailBackground.gameObject.SetActive(false);
        _gamePausedBackground.gameObject.SetActive(false);

        _keepGoingButton.gameObject.SetActive(false);
        _tryAgainButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(true);

        _menu.gameObject.SetActive(true);

        _menu.DOFade(1, 1f).OnComplete(() =>
        {
            IncreaseLevelIndex();
        });

        Debug.Log("Level Complete");
    }

    /// <summary>
    /// Marks the level as fail and shows the completion UI.
    /// </summary>
    public void LevelFail()
    {
        _levelFailBackground.gameObject.SetActive(true);
        _levelCompleteBackground.gameObject.SetActive(false);
        _gamePausedBackground.gameObject.SetActive(false);

        _keepGoingButton.gameObject.SetActive(false);
        _tryAgainButton.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(true);

        _menu.gameObject.SetActive(true);

        _menu.DOFade(1, 1f);

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
        _gamePausedBackground.gameObject.SetActive(true);

        _pauseButton.gameObject.SetActive(false);
        _continueButton.gameObject.SetActive(false);
        _tryAgainButton.gameObject.SetActive(false);
        _keepGoingButton.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);

        _menu.gameObject.SetActive(true);

        Sequence pauseSequence = DOTween.Sequence();
        pauseSequence.Append(_menu.DOFade(1, 0.5f))
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
        resumeSequence.Append(_menu.DOFade(0, 0.5f))
                      .OnComplete(() =>
                      {
                          _gamePausedBackground.gameObject.SetActive(false);
                          _menu.gameObject.SetActive(false);
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