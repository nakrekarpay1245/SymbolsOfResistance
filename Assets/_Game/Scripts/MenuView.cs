using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Manages the visibility and position of a menu in the game.
/// The menu will slide up from the bottom of the screen and fade in when opened,
/// and slide down and fade out when closed.
/// </summary>
public class MenuView : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("The main container of the menu.")]
    [SerializeField] private RectTransform _menuContainer;

    [Tooltip("The canvas group for handling the menu's visibility.")]
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Animation Settings")]
    [Tooltip("The duration of the open/close animation.")]
    [SerializeField][Range(0.1f, 2f)] private float _animationDuration = 0.5f;

    [Tooltip("The ease type for the animation.")]
    [SerializeField] private Ease _animationEase = Ease.OutCubic;

    private Vector2 _hiddenPosition;
    private Vector2 _visiblePosition;

    private void Awake()
    {
        _menuContainer = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        // Initialize positions
        _hiddenPosition = new Vector2(_menuContainer.anchoredPosition.x, -Screen.height);
        _visiblePosition = new Vector2(_menuContainer.anchoredPosition.x, 0);

        // Start with the menu hidden
        _menuContainer.anchoredPosition = _hiddenPosition;
        _canvasGroup.alpha = 0;
    }

    /// <summary>
    /// Opens the menu by sliding it up and fading it in.
    /// </summary>
    public void Open()
    {
        _menuContainer.DOAnchorPos(_visiblePosition, _animationDuration).SetEase(_animationEase);
        _canvasGroup.DOFade(1, _animationDuration).SetEase(_animationEase);
    }

    /// <summary>
    /// Closes the menu by sliding it down and fading it out.
    /// </summary>
    public void Close()
    {
        _menuContainer.DOAnchorPos(_hiddenPosition, _animationDuration).SetEase(_animationEase);
        _canvasGroup.DOFade(0, _animationDuration).SetEase(_animationEase);
    }
}