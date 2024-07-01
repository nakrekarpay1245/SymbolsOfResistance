using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Health Bar Params")]
    [Header("Reference")]
    [SerializeField]
    private AbstractDamagable _parentDamagable;

    [Header("UI")]
    [SerializeField]
    private Image _healthBarFill;

    private void Awake()
    {
        _parentDamagable = GetComponentInParent<AbstractDamagable>();
        _parentDamagable.OnHealthChanged += DisplayHealth;
    }

    private void DisplayHealth(float health, float maxHealth)
    {
        float targetFillAmount = health / maxHealth;
        _healthBarFill.DOFillAmount(targetFillAmount, 0.5f);
    }
}