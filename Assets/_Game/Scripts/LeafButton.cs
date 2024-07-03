using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// The LeafButton class handles the color change of an Image component when the mouse interacts with it 
/// using EventTrigger component.
/// </summary>
[RequireComponent(typeof(EventTrigger))]
public class LeafButton : MonoBehaviour
{
    [Header("Colors")]
    [Tooltip("Color when the mouse is over the button.")]
    [SerializeField] private Color hoverColor = new Color(190f, 190f, 179f);
    [Tooltip("Color when the button is pressed.")]
    [SerializeField] private Color pressedColor = new Color(175f, 175f, 175f);
    [Tooltip("Color when the mouse is not interacting with the button.")]
    [SerializeField] private Color normalColor = new Color(232f, 232f, 232f);

    private Image _buttonImage;
    private TextMeshProUGUI _buttonText;

    /// <summary>
    /// Initializes the Image component and sets up EventTrigger events.
    /// </summary>
    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        if (_buttonImage)
            _buttonImage.color = normalColor;

        _buttonText = GetComponent<TextMeshProUGUI>();
        if (_buttonText)
            _buttonText.color = normalColor;

        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }

        AddEventTrigger(eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
        AddEventTrigger(eventTrigger, EventTriggerType.PointerExit, OnPointerExit);
        AddEventTrigger(eventTrigger, EventTriggerType.PointerDown, OnPointerDown);
        AddEventTrigger(eventTrigger, EventTriggerType.PointerUp, OnPointerUp);
    }

    /// <summary>
    /// Adds an event trigger to the EventTrigger component.
    /// </summary>
    /// <param name="trigger">The EventTrigger component.</param>
    /// <param name="eventType">The type of event to trigger.</param>
    /// <param name="action">The callback function to invoke.</param>
    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((data) => action.Invoke((BaseEventData)data));
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// Changes the button color to hoverColor when the mouse enters the button.
    /// </summary>
    /// <param name="eventData">Pointer event data.</param>
    private void OnPointerEnter(BaseEventData eventData)
    {
        if (_buttonImage)
            _buttonImage.color = hoverColor;
        else if (_buttonText)
            _buttonText.color = hoverColor;
    }


    /// <summary>
    /// Changes the button color to normalColor when the mouse exits the button.
    /// </summary>
    /// <param name="eventData">Pointer event data.</param>
    private void OnPointerExit(BaseEventData eventData)
    {
        if (_buttonImage)
            _buttonImage.color = normalColor;
        else if (_buttonText)
            _buttonText.color = normalColor;
    }

    /// <summary>
    /// Changes the button color to pressedColor when the button is pressed.
    /// </summary>
    /// <param name="eventData">Pointer event data.</param>
    private void OnPointerDown(BaseEventData eventData)
    {
        if (_buttonImage)
            _buttonImage.color = pressedColor;
        else if (_buttonText)
            _buttonText.color = pressedColor;
    }

    /// <summary>
    /// Changes the button color back to hoverColor if the mouse is still over the button after release.
    /// </summary>
    /// <param name="eventData">Pointer event data.</param>
    private void OnPointerUp(BaseEventData eventData)
    {
        if (eventData is PointerEventData pointerEventData && pointerEventData.hovered.Contains(gameObject))
        {
            if (_buttonImage)
                _buttonImage.color = hoverColor;
            else if (_buttonText)
                _buttonText.color = hoverColor;
        }
        else
        {
            if (_buttonImage)
                _buttonImage.color = normalColor;
            else if (_buttonText)
                _buttonText.color = normalColor;
        }
    }
}