using UnityEngine;
using DG.Tweening;

public class PlayerBase : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField, Range(1, 3), Tooltip("Player's maximum health points")]
    private int _maxHealth = 3;

    [Header("Player Settings")]
    [SerializeField, Range(0, 1f), Tooltip("Player's maximum health points")]
    private float _heartAnimationDurtion = 0.35f;

    [Header("UI References")]
    [SerializeField, Tooltip("References to the heart icons in the UI")]
    private Transform[] _heartIcons;
    [SerializeField, Range(0, 1f), Tooltip("")]
    private float _damageScreenShowDuration = 0.25f;
    [SerializeField, Range(0, 1f), Tooltip("")]
    private float _damageScreenHideDuration = 0.75f;

    [SerializeField, Tooltip("The damage screen canvas group")]
    private CanvasGroup _damageScreen;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Reduces the player's health and updates the UI accordingly.
    /// </summary>
    /// <param name="damage">The amount of damage to take.</param>
    private void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        UpdateHealthUI();

        _damageScreen.DOFade(1, _damageScreenShowDuration).OnComplete(() =>
        {
            _damageScreen.DOFade(0, _damageScreenHideDuration);
        });

        if (_currentHealth <= 0)
        {
            LevelFail();
        }
    }

    /// <summary>
    /// Updates the health UI by scaling down the heart icons.
    /// </summary>
    private void UpdateHealthUI()
    {
        for (int i = _maxHealth - 1; i >= _currentHealth; i--)
        {
            if (i >= 0 && i < _heartIcons.Length)
            {
                _heartIcons[i].DOScale(0, _heartAnimationDurtion);
            }
        }
    }

    /// <summary>
    /// Handles the logic when the player fails the level.
    /// </summary>
    private void LevelFail()
    {
        // Implement the level fail logic here
        Debug.Log("Level Failed");
        GlobalBinder.singleton.LevelManager.LevelFail();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.gameObject.SetActive(false);
                TakeDamage(1);
            }
        }
    }
}