using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AbstractDamagable : MonoBehaviour, IDamagable
{
    [Header("Health Params")]
    [SerializeField] protected float _health;
    [SerializeField] protected float _maxHealth = 5;
    [SerializeField] protected float _scaleChangeTime = 0.25f;

    protected bool _isDie = false;

    [Header("Effects")]
    [Header("Particles")]
    [SerializeField]
    private string _damageParticleKey = "DamageParticle";
    [SerializeField]
    private string _deadParticleKey = "DeadParticle";
    [Header("Audios")]
    [SerializeField]
    private string _damageClipKey = "DamageClip";
    [SerializeField]
    private string _deadClipKey = "DeadClip";

    public delegate void HealthChangeHandler(float health, float maxHealth);
    public event HealthChangeHandler OnHealthChanged;

    private Collider2D _collider;

    public virtual void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public virtual void Start()
    {
        SetHealth();
    }

    public virtual void SetHealth()
    {
        _health = _maxHealth;
        RaiseHealthChangedEvent(_health, _maxHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0 && !_isDie)
        {
            Die();
        }

        //Play Effects
        PlayDamageParticle();
        PlayDamageSound();

        Vector3 popUpTextPosition = transform.position;

        string popUpText = "-" + damage.ToString();

        float popUpDuration = GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration +
            GlobalBinder.singleton.TimeManager.PopUpTextAnimationDelay;

        GlobalBinder.singleton.PopUpTextManager.ShowPopUpText(popUpTextPosition, popUpText, popUpDuration);

        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    protected virtual void RaiseHealthChangedEvent(float health, float maxHealth)
    {
        OnHealthChanged?.Invoke(health, maxHealth);
    }

    public virtual void Die()
    {
        StartCoroutine(DieRoutine());
    }

    public virtual IEnumerator DieRoutine()
    {
        _isDie = true;
        _collider.enabled = false;

        //Play Effects
        PlayDeadParticle();
        PlayDeadSound();
        transform.DOScale(0f, GlobalBinder.singleton.TimeManager.DamagableScaleChangeTime);
        Destroy(gameObject, GlobalBinder.singleton.TimeManager.DamagableScaleChangeTime);
        yield return new WaitForSeconds(GlobalBinder.singleton.TimeManager.DamagableDestroyDelay);
    }

    private void PlayDamageParticle()
    {
        GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_damageParticleKey, transform.position);
    }

    private void PlayDamageSound()
    {
        GlobalBinder.singleton.AudioManager.PlaySound(_damageClipKey, 0.5f);
    }

    private void PlayDeadParticle()
    {
        GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_deadParticleKey, transform.position);
    }

    private void PlayDeadSound()
    {
        GlobalBinder.singleton.AudioManager.PlaySound(_deadClipKey, 0.5f);
    }
}