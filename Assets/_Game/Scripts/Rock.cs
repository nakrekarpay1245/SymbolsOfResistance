using DG.Tweening;
using System;
using UnityEngine;

public class Rock : MonoBehaviour, IDamager, IDamagable
{
    [Header("Rock Params")]
    [Header("Attack  Params")]
    [SerializeField]
    private float _damage = 1f;

    [Header("Health  Params")]
    [SerializeField]
    private float _health = 1f;
    [SerializeField]
    private float _maxHorizontalDistance = 8f;

    /// <summary>
    /// // Rotation speed in degrees per second
    /// </summary>
    [Header("Rotation  Params")]
    [SerializeField]
    private float _rotationSpeed = 100f;

    //[Header("Effects")]
    //[Header("Damage")]
    //[SerializeField]
    //private string _damageParticleKey = "RockDamageParticle";
    //[SerializeField]
    //private string _damageClipKey = "RockDamageClip";
    //[Header("Die")]
    //[SerializeField]
    //private string _deadParticleKey = "RockDieParticle";
    //[SerializeField]
    //private string _deadClipKey = "RockDieClip";

    private Transform _rockBody;

    private void Awake()
    {
        _rockBody = GetComponentInChildren<SpriteRenderer>().transform;
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.25f);
    }

    private void Update()
    {
        RotateBody();
        DieForDistance();
    }

    private void DieForDistance()
    {
        if (transform.position.x >= _maxHorizontalDistance)
        {
            Debug.Log("HORIZOTAL DIE");
            Die();
        }
    }

    public void Damage(IDamagable damagable)
    {
        damagable.TakeDamage(_damage);
    }

    public void TakeDamage(float damage = 1f)
    {
        _health -= damage;
        if (_health <= 0f)
        {
            Die();
        }
        else
        {
            //GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_damageParticleKey, transform.position);
            //GlobalBinder.singleton.AudioManager.PlaySound(_damageClipKey);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        //GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_deadParticleKey, transform.position);
        //GlobalBinder.singleton.AudioManager.PlaySound(_deadClipKey);
    }

    private void RotateBody()
    {
        _rockBody.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            Damage(damagable);
            TakeDamage();
        }
    }
}
