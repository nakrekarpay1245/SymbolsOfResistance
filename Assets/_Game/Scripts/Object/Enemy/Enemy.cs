using DG.Tweening;
using UnityEngine;

public class Enemy : ObjectAnimator
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
    [Header("Particles")]
    [SerializeField]
    private string _attackParticleKey = "EnemyMeleeAttackParticle";
    [SerializeField]
    private string _attackClipKey = "EnemyMeleeAttackClip";

    [Header("Detection Params")]
    [SerializeField]
    private float _unitDetectionDistance = 0.35f;
    [SerializeField]
    private LayerMask _unitLayer;
    [SerializeField]
    private LayerMask _enemyLayer;
    [SerializeField]
    private float _enemyDetectionDistance = 1.1f;
    [SerializeField]
    private float _enemyDetectionRadius = 0.5f;

    //Components
    private Rigidbody2D _rigidbody;
    private ObjectAnimator _objectAnimator;

    //Private
    private bool _unitDetected;
    private bool _enemyDetected;
    private EnemyManager _enemyManager;

    public override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody2D>();
        _objectAnimator = GetComponent<ObjectAnimator>();

        _weapon = GetComponentInChildren<Weapon>();

        _enemyManager = GetComponentInParent<EnemyManager>();

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

    /// <summary>
    /// Handles the movement and detection logic of the unit.
    /// </summary>
    private void FixedUpdate()
    {
        Move();
        DetectEnemiesAndUnits();
    }

    /// <summary>
    /// Moves the unit to the left with a fixed speed.
    /// </summary>
    private void Move()
    {
        _rigidbody.velocity = -Vector3.right * _moveSpeed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Detects other units or enemies within detection ranges.
    /// </summary>
    private void DetectEnemiesAndUnits()
    {
        Vector2 origin = _weapon.transform.position;
        Vector2 direction = -Vector2.right;

        RaycastHit2D attackHit = Physics2D.Raycast(origin, direction, _unitDetectionDistance, _unitLayer);

        if (attackHit.collider != null)
        {
            HandleUnitDetection(attackHit.collider);
        }
        else
        {
            HandleNoUnitDetection();
        }
    }

    /// <summary>
    /// Handles the behavior when a unit is detected.
    /// </summary>
    /// <param name="unitCollider">Collider of the detected unit.</param>
    private void HandleUnitDetection(Collider2D unitCollider)
    {
        _unitDetected = true;
        SetSpeed(_attackSpeed);
        //Debug.Log($"Unit detected: {unitCollider.name}");
    }

    /// <summary>
    /// Handles the behavior when no unit is detected.
    /// </summary>
    private void HandleNoUnitDetection()
    {
        _unitDetected = false;

        Collider2D enemyCollider = Physics2D.OverlapCircle(transform.position +
            (Vector3.left * _enemyDetectionDistance), _enemyDetectionRadius, _enemyLayer);

        if (enemyCollider != null && enemyCollider.gameObject != gameObject)
        {
            HandleEnemyDetection(enemyCollider);
        }
        else
        {
            HandleNoEnemyDetection();
        }
    }

    /// <summary>
    /// Handles the behavior when an enemy is detected.
    /// </summary>
    /// <param name="enemyCollider">Collider of the detected enemy.</param>
    private void HandleEnemyDetection(Collider2D enemyCollider)
    {
        _enemyDetected = true;
        SetSpeed(_attackSpeed);
        //Debug.Log($"Enemy detected: {enemyCollider.name}");
    }

    /// <summary>
    /// Handles the behavior when no enemy is detected.
    /// </summary>
    private void HandleNoEnemyDetection()
    {
        _enemyDetected = false;
        SetSpeed(_walkSpeed);
        //Debug.Log("No enemy detected.");
    }

    /// <summary>
    /// Sets the speed of the unit.
    /// </summary>
    /// <param name="speed">Speed value to set.</param>
    private void SetSpeed(float speed)
    {
        _moveSpeed = speed;
        _objectAnimator.SetMoveSpeed(speed);
    }

    private void Attack()
    {
        GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_attackParticleKey, transform.position);
        GlobalBinder.singleton.AudioManager.PlaySound(_attackClipKey);

        //Debug.Log("Enemy attack animation");
        _objectAnimator.PlayAttackAnimation();
        _weapon.Attack();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        BackUp();
        _objectAnimator.PlayHurtAnimation();
    }

    public override void Die()
    {
        base.Die();
        _enemyManager.IncreaseDeadEnemyCount();
        _objectAnimator.PlayDeadAnimation();
    }

    private void BackUp()
    {
        Vector3 backUpPosition = transform.position + (Vector3.right * _backUpDistance);
        transform.DOMove(backUpPosition, _backUpDuration);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = _enemyDetected ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position + (Vector3.left * _enemyDetectionDistance), _enemyDetectionRadius);

        Gizmos.color = _unitDetected ? Color.red : Color.green;

        Vector3 origin = _weapon.transform.position;

        Vector3 direction = -Vector3.right;

        float rayDistance = _unitDetectionDistance;

        Vector3 endPoint = origin + direction * rayDistance;

        Gizmos.DrawLine(origin, endPoint);
    }
}