using DG.Tweening;
using UnityEngine;

public class Rock : MonoBehaviour, IDamager, IDamagable
{
    [Header("Rock Params")]
    [SerializeField]
    private float _damage = 1f;
    [SerializeField]
    private float _health = 1f;

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

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.25f);
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
        //GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_deadParticleKey, transform.position);
        //GlobalBinder.singleton.AudioManager.PlaySound(_deadClipKey);
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
