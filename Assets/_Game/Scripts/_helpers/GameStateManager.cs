using UnityEngine;

public class GameStateManager
{
    private static GameStateManager singleton;

    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;

    public GameState CurrentGameState { get; private set; }

    private GameStateManager() { }

    public static GameStateManager Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new GameStateManager();
            }
            return singleton;
        }
    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState) return;
        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}