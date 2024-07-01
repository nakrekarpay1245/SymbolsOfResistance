using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour, IDamager
{
    [Header("Weapon Params")]
    [SerializeField]
    private Collider2D _collider;

    private float _damage;
    public float DamageAmount { get => _damage; set => _damage = value; }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        DeactivateCollider();
    }

    public void Attack()
    {
        Sequence attackSequence = DOTween.Sequence();
        attackSequence.AppendCallback(() => ActivateCollider());
        attackSequence.AppendInterval(0.25f);
        attackSequence.AppendCallback(() => DeactivateCollider());
    }

    private void ActivateCollider()
    {
        //Debug.Log("Collider Activated!");
        _collider.enabled = true;
    }

    private void DeactivateCollider()
    {
        //Debug.Log("Collider Deactivated!");
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            //Debug.Log("Damage To: " + other.name);
            Damage(damagable);
        }
    }

    public void Damage(IDamagable damagable)
    {
        damagable.TakeDamage(_damage);
    }
}