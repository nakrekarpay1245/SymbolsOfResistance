using System;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //[Header("Manager Params")]
    public event Action OnLevelStarted;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;

    private bool _isLevelFinished;
    public void StartGame()
    {
        Debug.Log("Manager OnLevelStarted");

        _isLevelFinished = false;

        RenderSettings.fog = true;
        OnLevelStarted?.Invoke();
    }

    public void CompleteGame()
    {
        if (!_isLevelFinished)
        {
            Debug.Log("Manager OnLevelCompleted");
            _isLevelFinished = true;

            RenderSettings.fog = true;
            OnLevelCompleted?.Invoke();
        }
    }

    public void FailGame()
    {
        if (!_isLevelFinished)
        {
            Debug.Log("Manager OnLevelFailed");

            _isLevelFinished = true;

            RenderSettings.fog = true;
            OnLevelFailed?.Invoke();
        }
    }
}