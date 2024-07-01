using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("PauseController Params")]
    [SerializeField]
    private KeyCode _pauseKey = KeyCode.Escape;
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private Button _resumeButton;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            PauseGame();
        });

        _resumeButton.onClick.AddListener(() =>
        {
            ResumeGame();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            GameState currentState = GameStateManager.Singleton.CurrentGameState;
            GameState newGameState = currentState == GameState.Gameplay ? GameState.Pause : GameState.Gameplay;
            GameStateManager.Singleton.SetState(newGameState);
        }
    }

    public void PauseGame()
    {
        GameState newGameState = GameState.Pause;
        GameStateManager.Singleton.SetState(newGameState);
    }

    public void ResumeGame()
    {
        GameState newGameState = GameState.Gameplay;
        GameStateManager.Singleton.SetState(newGameState);
    }
}