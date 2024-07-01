using DG.Tweening;
using UnityEngine;

public class Enemy : AbstractDamagable
{
    [Header("Speed")]
    private float _moveSpeed = 25f;
    [SerializeField]
    private float _attackSpeed = 0f;
    [SerializeField]
    private float _walkSpeed = 25f;

    [Space]

    [Header("Back Up")]
    [SerializeField]
    private float _backUpDuration = 0.1f;
    [SerializeField]
    private float _backUpDistance = 0.25f;

    [Space]

    [Header("Attack")]
    [SerializeField]
    private Weapon _weapon;
    [SerializeField]
    private float _lastAttackTime;
    private float _attackTime = 5f;
    [SerializeField]
    private float _damage = 1f;
    [Header("Detection Params")]
    [SerializeField]
    private float _distance = 1f;
    [SerializeField]
    private LayerMask _unitLayer;

    //Components
    private Rigidbody2D _rigidbody;

    //Private
    private bool _unitDetected;
    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _weapon = GetComponentInChildren<Weapon>();
        _weapon.DamageAmount = _damage;
        SetSpeed(_walkSpeed);
    }

    private void OnEnable()
    {
        _lastAttackTime = Time.time + _attackTime;
    }

    private void Update()
    {
        if (_unitDetected)
        {
            if (_lastAttackTime <= Time.time)
            {
                _lastAttackTime = Time.time + _attackTime;
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = -Vector3.right * _moveSpeed * Time.fixedDeltaTime;

        Vector2 origin = transform.position;
        Vector2 direction = -Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _distance, _unitLayer);

        if (hit.collider != null)
        {
            _unitDetected = true;
            SetSpeed(_attackSpeed);
            //Debug.Log("Unit detected: " + hit.collider.name);
        }
        else
        {
            SetSpeed(_walkSpeed);
            _unitDetected = false;
            //Debug.Log("Unit not detected!");
        }
    }

    private void SetSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    private void Attack()
    {
        _weapon.Attack();
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

    void OnDrawGizmos()
    {
        Gizmos.color = _unitDetected ? Color.red : Color.green;
        Vector2 origin = transform.position;
        Vector2 direction = -Vector2.right;
        Gizmos.DrawLine(origin, origin + direction * _distance);
    }
}