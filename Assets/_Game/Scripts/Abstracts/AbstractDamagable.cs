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

    //[Header("Effects")]
    //[Header("Particles")]
    //[SerializeField]
    //private string _deadParticleKey = "DeadParticle";
    //[Header("Audios")]
    //[SerializeField]
    //private string _deadClipKey = "DeadClip";

    public delegate void HealthChangeHandler(float health, float maxHealth);
    public event HealthChangeHandler OnHealthChanged;

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

        //Play Effects
        PlayDeadParticle();
        PlayDeadSound();
        yield return new WaitForSeconds(GlobalBinder.singleton.TimeManager.DamagableDestroyDelay);

        // Shrink
        transform.DOScale(0f, GlobalBinder.singleton.TimeManager.DamagableScaleChangeTime);
        Destroy(gameObject);
    }

    private void PlayDeadParticle()
    {
        //GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_deadParticleKey, transform.position);
    }

    private void PlayDeadSound()
    {
        //GlobalBinder.singleton.AudioManager.PlaySound(_deadClipKey, 0.5f);
    }
}