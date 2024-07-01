using DG.Tweening;
using UnityEngine;

public class Enemy : AbstractDamagable
{
    [Header("Speed")]
    [SerializeField]
    private float _moveSpeed = 25f;

    [Header("Back Up")]
    [SerializeField]
    private float _backUpDuration = 0.1f;
    [SerializeField]
    private float _backUpDistance = 0.25f;

    //Components
    private Rigidbody2D _rigidbody;

    public override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = -Vector3.right * _moveSpeed * Time.fixedDeltaTime;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        BackUp();
    }

    private void BackUp()
    {
        Vector3 backUpPosition = transform.position + (Vector3.right * _backUpDistance);
        transform.DOMove(backUpPosition, _backUpDuration);
    }
}