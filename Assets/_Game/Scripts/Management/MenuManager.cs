using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    //PARAMS
    #region PARAMS
    [Header("MenuManager Params")]
    [Header("Start")]
    [SerializeField]
    private EventTrigger _startButton;

    [Header("Quit")]
    [SerializeField]
    private EventTrigger _quitButton;
    #endregion

    private void Awake()
    {
        AddEventTrigger(_startButton.gameObject, EventTriggerType.PointerClick, StartLevel);
        AddEventTrigger(_quitButton.gameObject, EventTriggerType.PointerClick, Quit);
    }

    //GAME
    #region GAME
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
    /// Adds an event trigger to a game object.
    /// </summary>
    /// <param name="obj">The game object to add the event trigger to.</param>
    /// <param name="type">The type of event trigger.</param>
    /// <param name="action">The action to invoke on the event.</param>
    private void AddEventTrigger(GameObject obj, EventTriggerType type, UnityEngine.Events.UnityAction action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }
}