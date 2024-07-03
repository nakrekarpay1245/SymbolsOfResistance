using UnityEngine;

public class UIElementArranger : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("UI Elements to be arranged")]
    [SerializeField] private RectTransform[] uiElements;

    [Header("Spacing Settings")]
    [Tooltip("Vertical space between each UI element")]
    [SerializeField, Range(0, 100)] private float verticalSpacing = 10f;

    [Header("Size Settings")]
    [Tooltip("New width for the UI elements")]
    [SerializeField, Range(100, 1000)] private float elementWidth = 200f;
    [Tooltip("New height for the UI elements")]
    [SerializeField, Range(50, 500)] private float elementHeight = 100f;

    /// <summary>
    /// Arranges the UI elements with specified spacing and size.
    /// </summary>
    private void Start()
    {
        ArrangeUIElements();
    }

    /// <summary>
    /// Sets the position and size of each UI element in the array.
    /// </summary>
    private void ArrangeUIElements()
    {
        if (uiElements == null || uiElements.Length == 0)
        {
            Debug.LogWarning("UI elements array is empty or not set.");
            return;
        }

        float currentYPosition = 0f;

        foreach (var uiElement in uiElements)
        {
            if (uiElement == null) continue;

            // Set the size of the UI element
            uiElement.sizeDelta = new Vector2(elementWidth, elementHeight);

            // Set the position of the UI element
            uiElement.anchoredPosition = new Vector2(uiElement.anchoredPosition.x, -currentYPosition);
            currentYPosition += uiElement.rect.height + verticalSpacing;
        }
    }

    /// <summary>
    /// Sets the UI elements to be arranged.
    /// </summary>
    /// <param name="elements">Array of RectTransform elements.</param>
    public void SetUIElements(RectTransform[] elements)
    {
        uiElements = elements;
        ArrangeUIElements();
    }

    /// <summary>
    /// Sets the vertical spacing between UI elements.
    /// </summary>
    /// <param name="spacing">Vertical space between elements.</param>
    public void SetVerticalSpacing(float spacing)
    {
        verticalSpacing = spacing;
        ArrangeUIElements();
    }

    /// <summary>
    /// Sets the size of the UI elements.
    /// </summary>
    /// <param name="width">New width for the elements.</param>
    /// <param name="height">New height for the elements.</param>
    public void SetElementSize(float width, float height)
    {
        elementWidth = width;
        elementHeight = height;
        ArrangeUIElements();
    }
}